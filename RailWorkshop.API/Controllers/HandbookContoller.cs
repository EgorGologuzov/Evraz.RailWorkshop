using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailWorkshop.Db.Utils;
using RailWorkshop.Services.Contracts;
using RailWorkshop.Services.Entity;
using RailWorkshop.Services.Interfaces;

namespace RailWorkshop.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/handbooks")]
    public class HandbookContoller : GeneralController
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<IHandbookEntity>))]
        public async Task<IActionResult> GetAll(string handbook)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            switch (handbook)
            {
                case _defectKey:
                    return await TypedGetAll<Defect>();

                case _railprofileKey:
                    return await TypedGetAll<RailProfile>();

                case _steelgradeKey:
                    return await TypedGetAll<SteelGrade>();

                case _workshopsegmentKey:
                    return await TypedGetAll<WorkshopSegment>();

                default:
                    return HandbookNotFound(handbook);
            }
        }

        [HttpGet("{handbook}/{id}")]
        [ProducesResponseType(200, Type = typeof(IHandbookEntity))]
        public async Task<IActionResult> GetById(string handbook, int id)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            switch (handbook)
            {
                case _defectKey:
                    return await TypedGetById<Defect>(id);

                case _railprofileKey:
                    return await TypedGetById<RailProfile>(id);

                case _steelgradeKey:
                    return await TypedGetById<SteelGrade>(id);

                case _workshopsegmentKey:
                    return await TypedGetById<WorkshopSegment>(id);

                default:
                    return HandbookNotFound(handbook);
            }
        }

        [Authorize(Roles = "Admin")]
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
                    return await TypedCreate<Defect>(data);

                case _railprofileKey:
                    return await TypedCreate<RailProfile>(data);

                case _steelgradeKey:
                    return await TypedCreate<SteelGrade>(data);

                case _workshopsegmentKey:
                    return await TypedCreate<WorkshopSegment>(data);

                default:
                    return HandbookNotFound(handbook);
            }
        }

        [Authorize(Roles = "Admin")]
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
                    return await TypedUpdate<Defect>(data);

                case _railprofileKey:
                    return await TypedUpdate<RailProfile>(data);

                case _steelgradeKey:
                    return await TypedUpdate<SteelGrade>(data);

                case _workshopsegmentKey:
                    return await TypedUpdate<WorkshopSegment>(data);

                default:
                    return HandbookNotFound(handbook);
            }
        }

        [Authorize(Roles = "Admin")]
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
                    return await TypedDelete<Defect>(data);

                case _railprofileKey:
                    return await TypedDelete<RailProfile>(data);

                case _steelgradeKey:
                    return await TypedDelete<SteelGrade>(data);

                case _workshopsegmentKey:
                    return await TypedDelete<WorkshopSegment>(data);

                default:
                    return HandbookNotFound(handbook);
            }
        }

        [NonAction]
        public async Task<IActionResult> TypedGetById<TEntity>(int id) where TEntity : class, IHandbookEntity
        {
            TEntity result = await _repos.GetById<TEntity>(id);

            if (result is null)
            {
                return EntityNotFound();
            }

            return Ok(result);
        }

        [NonAction]
        public async Task<IActionResult> TypedGetAll<TEntity>() where TEntity : class, IHandbookEntity
        {
            return Ok(await _repos.GetAll<TEntity>());
        }

        [NonAction]
        public async Task<IActionResult> TypedCreate<TEntity>(JsonElement data) where TEntity : class, IHandbookEntity
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
        public async Task<IActionResult> TypedUpdate<TEntity>(JsonElement data) where TEntity : class, IHandbookEntity
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
        public async Task<IActionResult> TypedDelete<TEntity>(JsonElement data) where TEntity : class, IHandbookEntity
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
    }
}