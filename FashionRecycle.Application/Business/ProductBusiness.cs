﻿using AutoMapper;
using FashionRecycle.API.Core.Entity;
using FashionRecycle.API.Core.Enums;
using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.Interface;
using FashionRecycle.API.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.Application.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IProductRepository _productRepository;
        public readonly IMapper _mapper;

        public ProductBusiness(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public ProductViewModel GetProductById(int idProduct)
        {
            if (idProduct != 0)
            {
                var resultEntity = _productRepository.GetProductById(idProduct);

                var result = _mapper.Map<ProductViewModel>(resultEntity);

                result.CreationDateFormat = resultEntity.CreationDate.ToString("dd/MM/yyyy");

                return result;
            }
            else
            {
                throw new Exception("Id de produto informado não é valido!");
            }
        }

        public List<ListAllProductsViewModel> GetListProductAll(ListAllProductsInputModel listAllProductsInputModel)
        {
            if (listAllProductsInputModel != null)
            {
                List<ListAllProductsViewModel> resultList = new List<ListAllProductsViewModel>();

                var resultEntity = _productRepository.GetProductAll(listAllProductsInputModel.Id, listAllProductsInputModel.IdBrand, listAllProductsInputModel.IdPartner);

                foreach (var product in resultEntity)
                {
                    var entity = _mapper.Map<ListAllProductsViewModel>(product);
                    if (entity != null)
                    {

                        entity.CreationDateFormat = entity.CreationDate.ToString("dd/MM/yyyy");
                        if(entity.ProductStatus == 1)
                        {
                            entity.ProductStatusDescription = "Disponivel";
                        } else if( entity.ProductStatus == 2)
                        {
                            entity.ProductStatusDescription = "Vendido";
                        }
                        else
                        {
                            entity.ProductStatusDescription = "Devolvido";
                        }
                        resultList.Add(entity);
                    }
                }

                return resultList;
            }
            else
            {
                throw new Exception("Objeto de input não pode ser nulo!");
            }
        }

        public void AlterOrCreateProduct(CreateProductInputModel inputModel)
        {
            if (inputModel != null && inputModel.id != 0)
            {
                var entity = _mapper.Map<ProductEntity>(inputModel);

                _productRepository.UpdateProduct(entity);
            }
            else if (inputModel != null)
            {
                var entity = _mapper.Map<ProductEntity>(inputModel);

                var numberProducts = _productRepository.CoutPartnerPorducts(inputModel.partnerId);

                if(numberProducts == 0)
                {
                    numberProducts = 1;

                    entity.ProductStatus = (int)ProductStatusEnum.available;

                    entity.AlternativeId = "FR." + inputModel.partnerId + "." + (numberProducts).ToString();
                }
                else
                {
                    entity.ProductStatus = (int)ProductStatusEnum.available;

                    entity.AlternativeId = "FR." + inputModel.partnerId + "." + (numberProducts + 1).ToString();
                }

                _productRepository.CreateProduct(entity);
            }
            else
            {
                throw new Exception("Objeto de input não pode ser nulo!");
            }
        }

        public List<ListProductsForSaleViewModel> GetProductAllForSale()
        {

            List<ListProductsForSaleViewModel> resultList = new List<ListProductsForSaleViewModel>();

            var resultEntity = _productRepository.GetProductAllForSale();

            foreach (var product in resultEntity)
            {
                var entity = _mapper.Map<ListProductsForSaleViewModel>(product);
                if (entity != null)
                {
                    resultList.Add(entity);
                }
            }

            return resultList;

        }

        public List<ListProductsForSaleViewModel> GetProductByPartnerForSale(int partnerId)
        {

            List<ListProductsForSaleViewModel> resultList = new List<ListProductsForSaleViewModel>();

            var resultEntity = _productRepository.GetProductByPartnerForSale(partnerId);

            foreach (var product in resultEntity)
            {
                var entity = _mapper.Map<ListProductsForSaleViewModel>(product);
                if (entity != null)
                {
                    resultList.Add(entity);
                }
            }

            return resultList;

        }

    }
}
