using FashionRecycle.API.Core.InputModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.ViewModel
{
    public class CreateSaleInputModel
    {
        public SaleInputModel? sale { get; set; }
        public List<SaleItemsInputModel>? saleItems { get; set; }
    }
}
