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
        List<PaymentsEntity> GetPaymentAll(string inicialDate, string finalDate);
        void CreatePayment(PaymentsEntity paymentsEntity);
        void UpdatePayment(PaymentsEntity paymentsEntity);
        double GetMargin();
        void DeletePayment(int idPayment);
    }
}
