using FashionRecycle.API.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Interface
{
    public interface IPaymentRepository
    {
        PaymentsEntity GetPaymentById(int paymentId);
        List<PaymentsEntity> GetPaymentAll(int paymentId, int idProvider, int idPartner);
        void CreatePayment(PaymentsEntity paymentsEntity);
        void UpdatePayment(PaymentsEntity paymentsEntity);
    }
}
