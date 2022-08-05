using FashionRecycle.API.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.ViewModel
{
    public class PaymentViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public PaymenyTypeEntity? PaymenyType { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentDateFormated { get; set; }
        public bool paymentMade { get; set; }
    }
}
