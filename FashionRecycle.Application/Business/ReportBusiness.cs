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

        public List<ReportAllPaymentsViewModel> GellAllPaymentsReport(string inicialDate, string finalDate, int idPaymentType)
        {
            var resultList = _reportRepository.GellAllPaymentsReport(inicialDate, finalDate, idPaymentType);

            return resultList;
        }
    }
}
