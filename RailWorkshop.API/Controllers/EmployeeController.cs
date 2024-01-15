using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailWorkshop.API.Dto;
using RailWorkshop.API.Utils;
using RailWorkshop.Db.Utils;
using RailWorkshop.Services.Contracts;
using RailWorkshop.Services.Entity;

namespace RailWorkshop.API.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : GeneralController<Employee>
    {
        private readonly IEmployeeRepository _repos;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public EmployeeController(
            IEmployeeRepository repos,
            ILogger<EmployeeController> logger,
            IMapper mapper) : base(repos, logger, mapper)
        {
            _repos = repos;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword([FromBody] EmployeePasswordDto data)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Employee result = await _repos.UpdatePassword(data.EmployeeId, data.OldPassword, data.NewPassword);
                EmployeeReturnDto dto = _mapper.Map<EmployeeReturnDto>(await _repos.GetById(result.Id));

                return Ok(dto);
            }
            catch (IncorrectLoginOrPasswordException)
            {
                return IncorrectLoginOrPassword();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                _logger.LogError("Failed password update {data}", data.ToJson());
                return InvalidData();
            }
        }

        [Authorize(Roles = ClientRoles.Admin)]
        [HttpDelete("password")]
        public async Task<IActionResult> ResetPassword([FromBody] EmployeePasswordDto data)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Employee result = await _repos.ResetPassword(data.EmployeeId, data.NewPassword);
                EmployeeReturnDto dto = _mapper.Map<EmployeeReturnDto>(await _repos.GetById(result.Id));

                return Ok(dto);
            }
            catch (EntityNotFoundException)
            {
                return EntityNotFound();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                _logger.LogError("Failed password reset {data}", data.ToJson());
                return InvalidData();
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            IActionResult result = await GetByIdGeneric(id);

            if (result is OkObjectResult r)
            {
                var value = (Employee)r.Value;
                r.Value = Mapper.Map<EmployeeReturnDto>(value);
            }

            return result;
        }

        [Authorize(Roles = ClientRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateDto data)
        {
            IActionResult result = await CreateGeneric(data);

            if (result is OkObjectResult r)
            {
                var value = (Employee)r.Value;
                r.Value = await GetReturnDtoByEntityId<EmployeeReturnDto>(value.Id);
            }

            return result;
        }

        [Authorize(Roles = ClientRoles.Admin)]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EmployeeUpdateDto data)
        {
            IActionResult result = await UpdateGeneric(data.Id, data);

            if (result is OkObjectResult r)
            {
                r.Value = await GetReturnDtoByEntityId<EmployeeReturnDto>(data.Id);
            }

            return result;
        }

        [Authorize(Roles = ClientRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            IActionResult result = await DeleteGeneric(id);

            if (result is OkObjectResult r)
            {
                r.Value = Mapper.Map<EmployeeReturnDto>(r.Value);
            }

            return result;
        }
    }
}