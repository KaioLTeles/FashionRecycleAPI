using FashionRecycle.API.Core.Entity;
using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Interface
{
    public interface IBrandBusiness
    {        
         List<BrandViewModel> GetBrandAll();
        void CreateBrand(CreateBrandInputModel inputModel);        
        void RemoveBrand(int brandId);
    }
}
