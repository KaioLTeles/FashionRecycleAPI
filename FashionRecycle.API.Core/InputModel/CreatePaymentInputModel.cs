using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.InputModel
{
    public class CreatePaymentInputModel
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int idPaymentType { get; set; }
        public double amount { get; set; }
        public string? paymentDate { get; set; }       
        public bool paymentMade { get; set; }
    }
}
