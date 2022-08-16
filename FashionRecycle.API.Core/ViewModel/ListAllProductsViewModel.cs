using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.ViewModel
{
    public class ListAllProductsViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public int AmountInventory { get; set; }
        public double PricePartner { get; set; }
        public double PriceSale { get; set; }
        public string? PartnerName { get; set; }
        public int ProductStatus { get; set; }
        public string? SerialNumber { get; set; }
        public string? AlternativeId { get; set; }
        public string? Model { get; set; }
        public string? Colour { get; set; }
        public int BrandId { get; set; }
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationDateFormat { get; set; }
        public double Margim { get; set; }
    }
}
