using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Entity
{
    public class PaymentsEntity : BaseEntity
    {
        public string Name { get; set; }
        public PaymenyTypeEntity? PaymenyType { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool Active { get; set; }
        public bool PaymentMade { get; set; }
    }
}
