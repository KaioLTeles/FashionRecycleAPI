using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Interface
{
    public interface IProductBusiness
    {
        ProductViewModel GetProductById(int idProduct);
        List<ListAllProductsViewModel> GetListProductAll(ListAllProductsInputModel listAllProductsInputModel);
        void AlterOrCreateProduct(CreateProductInputModel inputModel);
        List<ListProductsForSaleViewModel> GetProductAllForSale();
        List<ListProductsForSaleViewModel> GetProductByPartnerForSale(int partnerId);
    }
}
