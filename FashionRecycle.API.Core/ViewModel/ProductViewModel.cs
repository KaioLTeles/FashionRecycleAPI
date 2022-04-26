﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public int AmountInventory { get; set; }
        public double PricePartner { get; set; }
        public double PriceSale { get; set; }
        public int PartnerId { get; set; }
        public bool Active { get; set; }
    }
}