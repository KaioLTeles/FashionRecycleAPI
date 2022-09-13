using FashionRecycle.API.Core.Entity;
using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Interface
{
    public interface IReportBusiness
    {
        List<ReportAllSalesResumedViewModel> GetAllSalesResumed(ReportSalesInputModel inputModel);
        List<ReportAllPaymentsViewModel> GellAllPaymentsReport(string inicialDate, string finalDate, int idPaymentType, int filtertype);
        List<CashFlowReportViewModel> CashFlowReport(string inicialDate, string finalDate, bool onlyRevenue, bool onlyExpense, bool realFlow);
        List<RecievableEntity> GetReciavableAllReport(string inicialDate, string finalDate);
    }
}
