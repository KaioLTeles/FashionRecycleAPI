using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.Interface;
using FashionRecycle.Application.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FashionRecycle.API.Controllers
{
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportBusiness _reportBusiness;

        public ReportsController(IReportBusiness reportBusiness)
        {
            _reportBusiness = reportBusiness;
        }

        [HttpPost("getReportSales")]
        public IActionResult GetReportSales([FromBody] ReportSalesInputModel inputModel)
        {
            try
            {
                var result = _reportBusiness.GetAllSalesResumed(inputModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar o relatório de vendas - ", ex);
                return BadRequest("Erro ao buscar o relatório de vendas favor contactar a TI");
            }
        }

        [HttpGet("getReportPayments")]
        public IActionResult GetReportSales(string inicialDate, string finalDate, int idPaymentType, int filtertype)
        {
            try
            {
                var result = _reportBusiness.GellAllPaymentsReport(inicialDate, finalDate, idPaymentType, filtertype);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar o relatório de pagamentos - ", ex);
                return BadRequest("Erro ao buscar o relatório de pagamentos favor contactar a TI");
            }
        }

        [HttpGet("getAllSalesForCashFlow")]
        public IActionResult GetAllSalesForCashFlow(string inicialDate, string finalDate, bool onlyRevenue, bool onlyExpense)
        {
            try
            {
                var result = _reportBusiness.CashFlowReport(inicialDate, finalDate, onlyRevenue, onlyExpense);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar o relatório de fluxo de caixa - ", ex);
                return BadRequest("Erro ao buscar o relatório de fluxo de caixa favor contactar a TI");
            }
        }

    }
}
