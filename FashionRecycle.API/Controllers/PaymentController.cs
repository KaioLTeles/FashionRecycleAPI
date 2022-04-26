using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.Interface;
using FashionRecycle.Application.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FashionRecycle.API.Controllers
{
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentBusiness _paymentBusiness;

        public PaymentController(IPaymentBusiness paymentBusiness)
        {
            _paymentBusiness = paymentBusiness;
        }

        [HttpGet("getListPaymentsAll")]
        public IActionResult GetListPaymentsAll(int idPayment, int idPartner, int idProvider)
        {           
            try
            {
                var result = _paymentBusiness.GetListPaymentAll(idPayment,idPartner,idProvider);
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
    }
}
