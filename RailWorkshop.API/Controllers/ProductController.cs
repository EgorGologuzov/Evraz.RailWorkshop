using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailWorkshop.API.Dto;
using RailWorkshop.API.Utils;
using RailWorkshop.Services.Contracts;
using RailWorkshop.Services.Entity;

namespace RailWorkshop.API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : GeneralController<Product>
    {
        public ProductController(
            IProductRepository repos,
            ILogger<ProductController> logger,
            IMapper mapper) : base(repos, logger, mapper)
        {
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            IActionResult result = await GetByIdGeneric(id);

            if (result is OkObjectResult r)
            {
                Product value = (Product)r.Value;
                r.Value = Mapper.Map<ProductReturnDto>(value);
            }

            return result;
        }

        [Authorize(Roles = ClientRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto data)
        {
            IActionResult result = await CreateGeneric(data);

            if (result is OkObjectResult r)
            {
                Product value = (Product)r.Value;
                r.Value = await GetReturnDtoByEntityId<ProductReturnDto>(value.Id);
            }

            return result;
        }

        [Authorize(Roles = ClientRoles.Admin)]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateDto data)
        {
            IActionResult result = await UpdateGeneric(data.Id, data);

            if (result is OkObjectResult r)
            {
                r.Value = await GetReturnDtoByEntityId<ProductReturnDto>(data.Id);
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
                r.Value = Mapper.Map<ProductReturnDto>(r.Value);
            }

            return result;
        }
    }
}