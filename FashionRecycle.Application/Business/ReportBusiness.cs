using FashionRecycle.API.Core.Entity;
using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.Interface;
using FashionRecycle.API.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.Application.Business
{
    public class ReportBusiness : IReportBusiness
    {
        private readonly IReportRepository _reportRepository;

        public ReportBusiness(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public List<ReportAllSalesResumedViewModel> GetAllSalesResumed(ReportSalesInputModel inputModel)
        {
            if (inputModel != null)
            {
                var resultList = _reportRepository.GetAllSalesResumed(inputModel);

                return resultList;
            }
            else
            {
                throw new Exception("Objeto de input não pode ser nulo!");
            }

        }

        public List<ReportAllPaymentsViewModel> GellAllPaymentsReport(string inicialDate, string finalDate, int idPaymentType, int filtertype)
        {
            var resultList = _reportRepository.GellAllPaymentsReport(inicialDate, finalDate, idPaymentType, filtertype);

            return resultList;
        }

        public List<CashFlowReportViewModel> CashFlowReport(string inicialDate, string finalDate, bool onlyRevenue, bool onlyExpense, bool realFlow)
        {
            List <CashFlowReportViewModel> resultList = new List <CashFlowReportViewModel>();

            if (onlyRevenue == true && onlyExpense == true)
            {
                var sales = _reportRepository.GetAllSalesForCashFlow(inicialDate, finalDate, realFlow);

                var payments = _reportRepository.GetAllPaymentsCashFlow(inicialDate, finalDate, realFlow);

                var balance = _reportRepository.GetInicialAmout();

                CashFlowReportViewModel reportAllPaymentsViewModel = new CashFlowReportViewModel();

                reportAllPaymentsViewModel.Balance = balance;

                double AmountTotalRevenue = 0;
                double AmountTotalExpense = 0;
                double BalanceLeft = balance;

                resultList.Add(reportAllPaymentsViewModel);

                foreach (var payment in payments)
                {
                    CashFlowReportViewModel entity = new CashFlowReportViewModel();

                    //AmountTotalExpense = AmountTotalExpense + payment.AmountPayment;
                    BalanceLeft = BalanceLeft - payment.AmountPayment;
                    entity.RowDate = payment.PaymentDate;
                    entity.RowDateText = payment.PaymentDate.ToString("dd/MM/yyyy");
                    entity.ValueExpense = payment.AmountPayment;
                    entity.Balance = BalanceLeft;

                    resultList.Add (entity);
                }

                foreach (var sale in sales)
                {
                    CashFlowReportViewModel entity = new CashFlowReportViewModel();

                    AmountTotalRevenue = AmountTotalRevenue + sale.AmountSale;
                    BalanceLeft = BalanceLeft + AmountTotalRevenue;
                    entity.RowDate = sale.SaleDate;
                    entity.RowDateText = sale.SaleDate.ToString("dd/MM/yyyy");
                    entity.ValueRevenue = sale.AmountSale;
                    entity.Balance = BalanceLeft;

                    resultList.Add(entity);
                }
            }
            else if(onlyExpense == true)
            {                

                var payments = _reportRepository.GetAllPaymentsCashFlow(inicialDate, finalDate, realFlow);

                var balance = _reportRepository.GetInicialAmout();

                CashFlowReportViewModel reportAllPaymentsViewModel = new CashFlowReportViewModel();

                reportAllPaymentsViewModel.Balance = balance;

                double AmountTotal = 0;
                double BalanceLeft = balance;

                resultList.Add(reportAllPaymentsViewModel);

                foreach (var payment in payments)
                {
                    CashFlowReportViewModel entity = new CashFlowReportViewModel();

                    AmountTotal = payment.AmountPayment;
                    BalanceLeft = BalanceLeft - AmountTotal;
                    entity.RowDate = payment.PaymentDate;
                    entity.RowDateText = payment.PaymentDate.ToString("dd/MM/yyyy");
                    entity.ValueExpense = payment.AmountPayment;
                    entity.Balance = BalanceLeft;

                    resultList.Add(entity);
                }
            }
            else if (onlyRevenue)
            {
                var sales = _reportRepository.GetAllSalesForCashFlow(inicialDate, finalDate, realFlow);
                

                var balance = _reportRepository.GetInicialAmout();

                CashFlowReportViewModel reportAllPaymentsViewModel = new CashFlowReportViewModel();

                reportAllPaymentsViewModel.Balance = balance;

                double AmountTotal = 0;
                double BalanceLeft = balance;

                resultList.Add(reportAllPaymentsViewModel);                

                foreach (var sale in sales)
                {
                    CashFlowReportViewModel entity = new CashFlowReportViewModel();

                    AmountTotal = sale.AmountSale;
                    BalanceLeft = BalanceLeft + AmountTotal;
                    entity.RowDate = sale.SaleDate;
                    entity.RowDateText = sale.SaleDate.ToString("dd/MM/yyyy");
                    entity.ValueRevenue = sale.AmountSale;
                    entity.Balance = BalanceLeft;

                    resultList.Add(entity);
                }
            }

            resultList.OrderBy(x => x.RowDate);

            return resultList;
        }

        public List<RecievableEntity> GetReciavableAllReport(string inicialDate, string finalDate)
        {
            return _reportRepository.GetReciavableAllReport(inicialDate, finalDate);
        }
    }
}
