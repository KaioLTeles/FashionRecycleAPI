using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.Interface;
using FashionRecycle.Application.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionRecycle.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class RecievablesController : ControllerBase
    {
        private readonly IRecievablesBusiness _recievablesBusiness;

        public RecievablesController(IRecievablesBusiness recievablesBusiness)
        {
            _recievablesBusiness = recievablesBusiness;
        }

        [HttpGet("getListReceiablesAll")]
        public IActionResult GetReciavableAll(string inicialDate, string finalDate, int idClient)
        {
            try
            {
                var result = _recievablesBusiness.GetReciavableAll(inicialDate, finalDate, idClient);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar a lista de recebimentos - ", ex);
                return BadRequest("Erro ao buscar a lista de recebimentos favor contactar a TI");
            }
        }

        [HttpPost("updateReceievalbe/{idReceiable}")]
        public IActionResult UpdateReceiable([FromRoute] int idReceiable)
        {
            try
            {
                 _recievablesBusiness.UpdateReceiable(idReceiable);
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao alterar o Recebimento - ", ex);
                return BadRequest("Erro ao alterar o Recebimento favor contactar a TI");
            }
        }
    }
}
