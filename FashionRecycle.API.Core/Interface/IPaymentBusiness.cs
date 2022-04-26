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
        List<PaymentViewModel> GetListPaymentAll(int idPayment, int idPartner, int idProvider);
        void AlterOrCreatePayment(CreatePaymentInputModel inputModel);
    }
}
