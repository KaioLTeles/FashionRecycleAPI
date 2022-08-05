using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.ViewModel
{
    public class AllPaymentsCashFlowViewModel
    {
        public DateTime PaymentDate { get; set; }
        public string PaymentDateText { get; set; }
        public double AmountPayment { get; set; }
    }
}
