using FashionRecycle.API.Core.Interface;
using FashionRecycle.API.Core.ViewModel;
using FashionRecycle.Application.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionRecycle.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesBusiness _salesBusiness;

        public SalesController(ISalesBusiness salesBusiness)
        {
            _salesBusiness = salesBusiness;
        }

        [HttpPost("createSale")]
        public IActionResult CreateSale([FromBody] CreateSaleInputModel inputModel)
        {
            try
            {
                int saleId = _salesBusiness.CreateSale(inputModel);
                return Ok(saleId);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao realizar venda", ex);
                return BadRequest("Erro ao realizar venda favor contactar a TI");
            }
        }
    }
}
