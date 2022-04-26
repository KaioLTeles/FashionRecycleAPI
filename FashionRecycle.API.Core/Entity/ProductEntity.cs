using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Entity
{
    public class ProductEntity : BaseEntity
    {
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public int AmountInventory { get; set; }
        public double PricePartner { get; set; }
        public double PriceSale { get; set; }
        public PartnerEntity? Partner { get; set; }
        public bool Active { get; set; }
    }
}
