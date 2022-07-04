using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.InputModel
{
    public class ListAllProductsInputModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int IdBrand { get; set; }
        public int IdPartner { get; set; }
    }
}
