using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Entity
{
    public class SalesEntity : BaseEntity
    {
        public int IdClient { get; set; }
        public double AmountSale { get; set; }
        public int IdPaymentMethod { get; set; }
        public string? Observation { get; set; }
        public bool Active { get; set; }
    }
}
