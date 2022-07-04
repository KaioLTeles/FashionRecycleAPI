using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Entity
{
    public class BrandEntity : BaseEntity
    {        
        public string? Name { get; set; }
        public bool Active { get; set; }
    }
}
