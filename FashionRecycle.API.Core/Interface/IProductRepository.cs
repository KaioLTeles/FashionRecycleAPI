using FashionRecycle.API.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Interface
{
    public interface IProductRepository
    {
        ProductEntity GetProductById(int productId);
        List<ProductEntity> GetProductAll(string productId, int idBrand, int idPartner);
        void CreateProduct(ProductEntity productEntity);
        void UpdateProduct(ProductEntity productEntity);
        List<ProductEntity> GetProductAllForSale();
        void UpdateProductStatus(int idProduct);
        int CoutPartnerPorducts(int partnerId);
    }
}
