﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.ViewModel
{
    public class PartnerViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? CPF { get; set; }
        public string? CNPJ { get; set; }
        public string? Address { get; set; }
        public string? StreetNumber { get; set; }
        public string? CEP { get; set; }
        public bool Active { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? DateOfBirthFormat { get; set; }
    }
}
