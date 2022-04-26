using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Entity
{
    public class SalesItemsEntity : BaseEntity
    {
        public int IdSales { get; set; }
        public int IdProduct { get; set; }
        public int IdPartner { get; set; }
        public double PriceSale { get; set; }
        public int Amount { get; set; }
        public bool Active { get; set; }
    }
}
