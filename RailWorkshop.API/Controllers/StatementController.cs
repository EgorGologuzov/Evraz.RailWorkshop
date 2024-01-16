using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailWorkshop.API.Dto;
using RailWorkshop.API.Utils;
using RailWorkshop.Services.Contracts;
using RailWorkshop.Services.Entity;

namespace RailWorkshop.API.Controllers
{
    [Route("api/statement")]
    [ApiController]
    public class StatementController : GeneralController<Statement>
    {
        private readonly IEmployeeRepository _employeeRepos;

        public StatementController(
            IStatementRepository repos,
            IEmployeeRepository employeeRepos,
            ILogger<StatementController> logger,
            IMapper mapper) : base(repos, logger, mapper)
        {
            _employeeRepos = employeeRepos;
        }

        [Authorize(Roles = ClientRoles.Admin)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            IActionResult result = await GetByIdGeneric(id);

            if (result is OkObjectResult r)
            {
                var value = (Statement)r.Value;
                r.Value = Mapper.Map<StatementReturnDto>(value);
            }

            return result;
        }

        [Authorize(Roles = ClientRoles.Employee)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StatementCreateDto data)
        {
            Claim claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid);
            Guid id = new(claim.Value);
            Employee employee = await _employeeRepos.GetById(id);

            data.Date = DateTime.Now;
            data.ResponsibleId = employee.Id;
            data.SegmentId = employee.SegmentId;

            IActionResult result = await CreateGeneric(data);

            if (result is OkObjectResult r)
            {
                var value = (Statement)r.Value;
                r.Value = await GetReturnDtoByEntityId<StatementReturnDto>(value.Id);
            }

            return result;
        }
    }
}