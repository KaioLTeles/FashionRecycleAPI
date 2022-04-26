using FashionRecycle.API.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Interface
{
    public interface ISalesRepository
    {
        int CreateSale(SalesEntity salesEntity, List<SalesItemsEntity> salesItemsEntity);
    }
}
