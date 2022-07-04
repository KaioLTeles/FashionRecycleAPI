using AutoMapper;
using FashionRecycle.API.Core.Entity;
using FashionRecycle.API.Core.Interface;
using FashionRecycle.API.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.Application.Business
{
    public class SalesBusiness : ISalesBusiness
    {

        private readonly ISalesRepository _salesRepository;
        private readonly IProductRepository _productRepository;
        public readonly IMapper _mapper;

        public SalesBusiness(ISalesRepository salesRepository, IMapper mapper, IProductRepository productRepository)
        {
            _salesRepository = salesRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public int CreateSale(CreateSaleInputModel inputModel)
        {
            if(inputModel != null)
            {
                var saleInput = inputModel.sale;
                var saleItemInput = inputModel.saleItems;
                List<SalesItemsEntity> saleItemEntityList = new List<SalesItemsEntity>();
                    
                var saleEntity = _mapper.Map<SalesEntity>(saleInput);
                foreach(var item in saleItemInput)
                {
                    var saleItemEntity = _mapper.Map<SalesItemsEntity>(item);
                    saleItemEntityList.Add(saleItemEntity);
                }                

                if (saleEntity != null && saleItemEntityList != null && saleItemEntityList.Count > 0)
                {
                    int saleId = _salesRepository.CreateSale(saleEntity, saleItemEntityList);

                    foreach(var item in saleItemEntityList)
                    {
                        _productRepository.UpdateProductStatus(item.IdProduct);
                    }

                    return saleId;
                }
                else
                {
                    throw new Exception("Objeto de cabeçalho e items não pode ser nulo!");
                }
            }
            else
            {
                throw new Exception("Objeto de vendas não pode ser nulo!");
            }
        }
    }
}
