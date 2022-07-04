using FashionRecycle.API.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Entity
{
    public class PaymenyTypeEntity : BaseEntity
    {
        public string? Description { get; set; }
        public PaymentTypeEnum Type { get; set; }
        public bool Active { get; set; }
    }
}
