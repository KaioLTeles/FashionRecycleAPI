using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.Interface;
using FashionRecycle.Application.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionRecycle.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")] 
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentBusiness _paymentBusiness;

        public PaymentController(IPaymentBusiness paymentBusiness)
        {
            _paymentBusiness = paymentBusiness;
        }

        [HttpGet("getListPaymentsAll")]
        public IActionResult GetListPaymentsAll(string inicialDate, string finalDate)
        {           
            try
            {
                var result = _paymentBusiness.GetListPaymentAll(inicialDate, finalDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar a lista de pagamentos - ", ex);
                return BadRequest("Erro ao buscar a lista de pagamentos favor contactar a TI");
            }
        }

        [HttpGet("getPaymentById")]
        public IActionResult GetPaymentById(int idPayment)
        {
            try
            {
                var result = _paymentBusiness.GetPaymentById(idPayment);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar os dados do pagamento - ", ex);
                return BadRequest("Erro ao buscar os dados do pagamento favor contactar a TI");
            }
        }

        [HttpPost("alterOrCreatePayment")]
        public IActionResult AlterOrCreatePayment([FromBody] CreatePaymentInputModel inputModel)
        {
            try
            {
                _paymentBusiness.AlterOrCreatePayment(inputModel);
                return Ok("Pagamento processado com sucesso!");
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao criar/alterar o pagamento " + inputModel.id + " - ", ex);
                return BadRequest("Erro ao criar/alterar o pagamento favor contactar a TI");
            }
        }

        [HttpGet("getMargin")]
        public IActionResult GetMargin()
        {
            try
            {
                var result = _paymentBusiness.GetMargin();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar a margem - ", ex);
                return BadRequest("Erro ao buscar a margem favor contactar a TI");
            }
        }

        [HttpDelete("removePayment/{paymentId}")]
        public IActionResult RemoveBrand([FromRoute] int paymentId)
        {
            try
            {
                _paymentBusiness.DeletePayment(paymentId);

                return Ok();
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao remover Pagamento - ", ex);
                return BadRequest("Erro ao remover Pagamento contactar a TI");
            }
        }
    }
}
