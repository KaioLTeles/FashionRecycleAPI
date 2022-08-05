using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Interface
{
    public interface IReportRepository
    {
        List<ReportAllSalesResumedViewModel> GetAllSalesResumed(ReportSalesInputModel inputModel);
        List<ReportAllPaymentsViewModel> GellAllPaymentsReport(string inicialDate, string finalDate, int idPaymentType, int filtertype);
        public double GetInicialAmout();
        List<AllPaymentsCashFlowViewModel> GetAllPaymentsCashFlow(string inicialDate, string finalDate);
        List<AllSalesForCashFlowViewModel> GetAllSalesForCashFlow(string inicialDate, string finalDate);


    }
}
