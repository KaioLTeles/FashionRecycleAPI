﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.InputModel
{
    public class CreateProviderInputModel
    {
        public int id { get; set; }
        public string? companyName { get; set; }
        public string? legalCompanyName { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }        
        public string? cnpj { get; set; }
        public string? address { get; set; }
        public string? streetNumber { get; set; }
        public string? cep { get; set; }
        public bool active { get; set; }
    }
}
