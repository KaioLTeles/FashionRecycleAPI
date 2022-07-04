using FashionRecycle.API.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Interface
{
    public interface IBrandRepository
    {
        List<BrandEntity> GetBrandAll();
        void CreateBrand(BrandEntity brandEntity);
        void UpdateBrand(BrandEntity brandEntity);
        void RemoveBrand(int brandId);
        bool CheckStatusRemoveBrand(int brandId);

    }
}
