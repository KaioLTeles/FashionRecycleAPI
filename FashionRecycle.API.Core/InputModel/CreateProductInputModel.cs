﻿using System;
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
        public int amountInventory { get; set; }
        public double pricePartner { get; set; }
        public double priceSale { get; set; }
        public int partnerId { get; set; }
        public bool active { get; set; }
        public int productStatus { get; set; }
        public string? serialNumber { get; set; }        
        public string? model { get; set; }
        public string? colour { get; set; }
        public string? observation { get; set; }
        public int brandId { get; set; }
        public double margim { get; set; }       
        public string? Content { get; set; }
        public string? Size { get; set; }
        public string? Size_BR { get; set; }
        public string? Size_Sola { get; set; }
        public string? ItemRelated { get; set; }
        public string? ApprovalDate { get; set; }
        public string? FileName { get; set; }
    }
}
