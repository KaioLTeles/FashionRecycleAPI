using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.ViewModel
{
    public class SaleInputModel
    {
        public int IdClient { get; set; }
        public double AmountSale { get; set; }
        public int IdPaymentMethod { get; set; }
        public string? Observation { get; set; }
    }
}
