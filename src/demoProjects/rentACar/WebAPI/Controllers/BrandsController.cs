using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateBrandCommand createSomeFeatureEntityCommand)
        {
            CreatedBrandEntityDto result = await Mediator.Send(createSomeFeatureEntityCommand);
            return Created("", result);
        }
    }
}
