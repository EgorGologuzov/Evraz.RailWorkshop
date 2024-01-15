using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RailWorkshop.API.Dto;
using RailWorkshop.Db.Utils;
using RailWorkshop.Services.Contracts;
using RailWorkshop.Services.Entity;

namespace RailWorkshop.API.Controllers
{
    public class GeneralController<TEntity> : ControllerBase
    {
        protected readonly IRepository<TEntity> Repository;
        protected readonly ILogger Logger;
        protected readonly IMapper Mapper;

        public GeneralController()
        {
        }

        public GeneralController(
            IRepository<TEntity> repos,
            ILogger logger,
            IMapper mapper)
        {
            Repository = repos;
            Logger = logger;
            Mapper = mapper;
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
        public IActionResult IncorrectLoginOrPassword()
        {
            ModelState.AddModelError("error", "Incorrect login or password");
            return BadRequest(ModelState);
        }

        [NonAction]
        protected virtual async Task<TReturnDto> GetReturnDtoByEntityId<TReturnDto>(object id)
        {
            return Mapper.Map<TReturnDto>(await Repository.GetById(id));
        }

        [NonAction]
        protected virtual async Task<IActionResult> GetByIdGeneric(object id)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            TEntity entity = await Repository.GetById(id);

            if (entity is null)
            {
                return EntityNotFound();
            }

            return Ok(entity);
        }

        [NonAction]
        protected virtual async Task<IActionResult> CreateGeneric<TCreateDto>(TCreateDto data)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            TEntity entity = Mapper.Map<TCreateDto, TEntity>(data);

            try
            {
                TEntity result = await Repository.Create(entity);

                return Ok(result);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Logger.LogError("Failed create {type} with data {data}", typeof(TEntity).Name, data.ToJson());
                return InvalidData();
            }
        }

        [NonAction]
        protected virtual async Task<IActionResult> UpdateGeneric<TUpdateDto>(object id, TUpdateDto data)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            TEntity entity = await Repository.GetById(id);
            Mapper.Map(data, entity);

            try
            {
                TEntity result = await Repository.Update(id, entity);

                return Ok(result);
            }
            catch (EntityNotFoundException)
            {
                return EntityNotFound();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Logger.LogError("Failed update {type} with data {data}", typeof(TEntity).Name, data.ToJson());
                return InvalidData();
            }
        }

        [NonAction]
        protected virtual async Task<IActionResult> DeleteGeneric(object id)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            try
            {
                TEntity result = await Repository.Delete(id);

                return Ok(result);
            }
            catch (EntityNotFoundException)
            {
                return EntityNotFound();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Logger.LogError("Failed delete {type} with data {id}", typeof(TEntity).Name, id);
                return InvalidData();
            }
        }
    }
}