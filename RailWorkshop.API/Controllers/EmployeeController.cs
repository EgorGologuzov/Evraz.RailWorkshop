using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailWorkshop.API.Dto;
using RailWorkshop.Db.Utils;
using RailWorkshop.Services.Contracts;
using RailWorkshop.Services.Entity;

namespace RailWorkshop.API.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : GeneralController
    {
        private readonly IEmployeeRepository _repos;
        private readonly ILogger _logger;
        private readonly IHandbookRepository _handbooks;

        public EmployeeController(
            IEmployeeRepository repos,
            IHandbookRepository handbooks,
            ILogger<EmployeeController> logger)
        {
            _repos = repos;
            _logger = logger;
            _handbooks = handbooks;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            Employee result = await _repos.GetById(id);

            if (result is null)
            {
                return EntityNotFound();
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordUpdateDto data)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Employee result = await _repos.UpdatePassword(data.EmployeeId, data.OldPassword, data.NewPassword);

                return Ok(result);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                _logger.LogError("Failed create Employee creation data", data.ToJson());
                return InvalidData();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Employee data)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Employee result = await _repos.Create(data);

                return Ok(result);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                _logger.LogError("Failed create Employee creation data {data}", data.ToJson());
                return InvalidData();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Employee data)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Employee result = await _repos.Update(data);

                return Ok(result);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                _logger.LogError("Failed update Employee creation data {data}", data.ToJson());
                return InvalidData();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Employee data)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Employee result = await _repos.Delete(data);

                return Ok(result);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                _logger.LogError("Failed delete Employee creation data {data}", data.ToJson());
                return InvalidData();
            }
        }
    }
}