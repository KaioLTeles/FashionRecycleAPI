using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.InputModel
{
    public class CreateProductInputModel
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? brand { get; set; }
        public int amountInventory { get; set; }
        public double pricePartner { get; set; }
        public double priceSale { get; set; }
        public int partnerId { get; set; }
        public bool active { get; set; }
    }
}
