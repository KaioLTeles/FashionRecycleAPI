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
    public interface IPaymentBusiness
    {
        PaymentViewModel GetPaymentById(int idPayment);
        List<PaymentViewModel> GetListPaymentAll(string inicialDate, string finalDate);
        void AlterOrCreatePayment(CreatePaymentInputModel inputModel);
        double GetMargin();
        void DeletePayment(int idPayment);
        void CreatePaymentPartner(List<SalesItemsEntity> salesItemsEntity);
    }
}
