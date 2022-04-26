using FashionRecycle.API.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Interface
{
    public interface ISalesBusiness
    {
        int CreateSale(CreateSaleInputModel inputModel);
    }
}
