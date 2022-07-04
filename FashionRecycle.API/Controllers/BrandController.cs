using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.Interface;
using FashionRecycle.Application.Utils;
using Microsoft.AspNetCore.Mvc;
namespace FashionRecycle.API.Controllers
{
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandBusiness _brandBusiness;

        public BrandController(IBrandBusiness brandBusiness)
        {
            _brandBusiness = brandBusiness;
        }

        [HttpGet("getBrandAll")]
        public IActionResult GetBrandAll()
        {            
            try
            {
                var result = _brandBusiness.GetBrandAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar a lista de marcas - ", ex);
                return BadRequest("Erro ao buscar a lista de marcas favor contactar a TI");
            }
        }

        [HttpPost("createBrand")]
        public IActionResult CreateBrand([FromBody] CreateBrandInputModel createBrandInputModel)
        {
            try
            {               
                _brandBusiness.CreateBrand(createBrandInputModel);

                return Ok();
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao criar/editar uma marca - ", ex);
                return BadRequest("Erro ao criar/editar uma marca favor contactar a TI");
            }
        }

        [HttpDelete("removeBrand/{brandId}")]
        public IActionResult RemoveBrand([FromRoute] int brandId)
        {
            try
            {
                _brandBusiness.RemoveBrand(brandId);

                return Ok();
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao remover a marca - ", ex);
                return BadRequest("Erro ao remover a favor contactar a TI");
            }
        }
    }    
}
