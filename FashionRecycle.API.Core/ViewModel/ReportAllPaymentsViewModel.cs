using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.ViewModel
{
    public class ReportAllPaymentsViewModel
    {
        public DateTime CreationDate { get; set; }
        public string? CreationDateFormated { get; set; }
        public string? Name { get; set; }
        public string? PaymentDescription { get; set; }
        public double Amount { get; set; }        
        public DateTime PaymentDate { get; set; }
        public string? PaymentDateFormated { get; set; }
    }
}
