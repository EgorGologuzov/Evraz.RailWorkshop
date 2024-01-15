using System.Data;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailWorkshop.API.Utils;
using RailWorkshop.Db.Utils;
using RailWorkshop.Services.Contracts;
using RailWorkshop.Services.Entity;
using RailWorkshop.Services.Interfaces;

namespace RailWorkshop.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/handbooks")]
    public class HandbookContoller : ControllerBase
    {
        private const string _defectKey = "defect";
        private const string _railprofileKey = "railprofile";
        private const string _steelgradeKey = "steelgrade";
        private const string _workshopsegmentKey = "workshopsegment";

        private readonly IHandbookRepository _repos;
        private readonly ILogger<HandbookContoller> _logger;

        public HandbookContoller(IHandbookRepository handbookRepository, ILogger<HandbookContoller> logger)
        {
            _repos = handbookRepository;
            _logger = logger;
        }

        [HttpGet("{handbook}")]
        public async Task<IActionResult> GetAll(string handbook)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            switch (handbook)
            {
                case _defectKey:
                    return await GetAllGeneric<Defect>();

                case _railprofileKey:
                    return await GetAllGeneric<RailProfile>();

                case _steelgradeKey:
                    return await GetAllGeneric<SteelGrade>();

                case _workshopsegmentKey:
                    return await GetAllGeneric<WorkshopSegment>();

                default:
                    return HandbookNotFound(handbook);
            }
        }

        [HttpGet("{handbook}/{id}")]
        public async Task<IActionResult> GetById(string handbook, int id)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            switch (handbook)
            {
                case _defectKey:
                    return await GetByIdGeneric<Defect>(id);

                case _railprofileKey:
                    return await GetByIdGeneric<RailProfile>(id);

                case _steelgradeKey:
                    return await GetByIdGeneric<SteelGrade>(id);

                case _workshopsegmentKey:
                    return await GetByIdGeneric<WorkshopSegment>(id);

                default:
                    return HandbookNotFound(handbook);
            }
        }

        [Authorize(Roles = ClientRoles.Admin)]
        [HttpPost("{handbook}")]
        public async Task<IActionResult> Create(string handbook, [FromBody] JsonElement data)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            switch (handbook)
            {
                case _defectKey:
                    return await CreateGeneric<Defect>(data);

                case _railprofileKey:
                    return await CreateGeneric<RailProfile>(data);

                case _steelgradeKey:
                    return await CreateGeneric<SteelGrade>(data);

                case _workshopsegmentKey:
                    return await CreateGeneric<WorkshopSegment>(data);

                default:
                    return HandbookNotFound(handbook);
            }
        }

        [Authorize(Roles = ClientRoles.Admin)]
        [HttpPut("{handbook}")]
        public async Task<IActionResult> Update(string handbook, [FromBody] JsonElement data)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            switch (handbook)
            {
                case _defectKey:
                    return await UpdateGeneric<Defect>(data);

                case _railprofileKey:
                    return await UpdateGeneric<RailProfile>(data);

                case _steelgradeKey:
                    return await UpdateGeneric<SteelGrade>(data);

                case _workshopsegmentKey:
                    return await UpdateGeneric<WorkshopSegment>(data);

                default:
                    return HandbookNotFound(handbook);
            }
        }

        [Authorize(Roles = ClientRoles.Admin)]
        [HttpDelete("{handbook}")]
        public async Task<IActionResult> Delete(string handbook, [FromBody] JsonElement data)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            switch (handbook)
            {
                case _defectKey:
                    return await DeleteGeneric<Defect>(data);

                case _railprofileKey:
                    return await DeleteGeneric<RailProfile>(data);

                case _steelgradeKey:
                    return await DeleteGeneric<SteelGrade>(data);

                case _workshopsegmentKey:
                    return await DeleteGeneric<WorkshopSegment>(data);

                default:
                    return HandbookNotFound(handbook);
            }
        }

        [NonAction]
        public async Task<IActionResult> GetByIdGeneric<TEntity>(int id) where TEntity : class, IHandbookEntity
        {
            TEntity result = await _repos.GetById<TEntity>(id);

            if (result is null)
            {
                return EntityNotFound();
            }

            return Ok(result);
        }

        [NonAction]
        public async Task<IActionResult> GetAllGeneric<TEntity>() where TEntity : class, IHandbookEntity
        {
            return Ok(await _repos.GetAll<TEntity>());
        }

        [NonAction]
        public async Task<IActionResult> CreateGeneric<TEntity>(JsonElement data) where TEntity : class, IHandbookEntity
        {
            TEntity entity = data.FromJson<TEntity>();

            try
            {
                TEntity result = await _repos.Create(entity);

                return Ok(result);
            }
            catch (RepeatingIdException)
            {
                return IdConflict();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                _logger.LogError("Failed create {type}, creation data {data}", typeof(TEntity).Name, data);

                return InvalidData();
            }
        }

        [NonAction]
        public async Task<IActionResult> UpdateGeneric<TEntity>(JsonElement data) where TEntity : class, IHandbookEntity
        {
            TEntity entity = data.FromJson<TEntity>();

            try
            {
                TEntity result = await _repos.Update(entity);

                return Ok(result);
            }
            catch (EntityNotFoundException)
            {
                return EntityNotFound();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                _logger.LogError("Failed update {type} updated data {newData}", typeof(TEntity).Name, data);

                return InvalidData();
            }
        }

        [NonAction]
        public async Task<IActionResult> DeleteGeneric<TEntity>(JsonElement data) where TEntity : class, IHandbookEntity
        {
            TEntity entity = data.FromJson<TEntity>();

            try
            {
                TEntity result = await _repos.Delete(entity);

                return Ok(result);
            }
            catch (EntityNotFoundException)
            {
                return EntityNotFound();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                _logger.LogError("Failed delete {type} deleted data {newData}", typeof(TEntity).Name, data);

                return InvalidData();
            }
        }

        [NonAction]
        public IActionResult HandbookNotFound(string handbook)
        {
            return NotFound($"Handbook with name \"{handbook}\" not found");
        }

        [NonAction]
        public IActionResult EntityNotFound()
        {
            return NotFound("Entity with that identification data not found");
        }

        [NonAction]
        public IActionResult InvalidData()
        {
            ModelState.AddModelError("error", "Mistake in data. May be some field skiped or has wrong data format or some id not exists.");
            return BadRequest(ModelState);
        }

        [NonAction]
        public IActionResult IdConflict()
        {
            ModelState.AddModelError("error", "Entity with that id already exists");
            return BadRequest(ModelState);
        }
    }
}