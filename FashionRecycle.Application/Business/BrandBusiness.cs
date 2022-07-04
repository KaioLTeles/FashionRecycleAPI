using AutoMapper;
using FashionRecycle.API.Core.Entity;
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
    public class BrandBusiness : IBrandBusiness
    {
        private readonly IBrandRepository _brandRepository;
        public readonly IMapper _mapper;

        public BrandBusiness(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public List<BrandViewModel> GetBrandAll()
        {
            List<BrandViewModel> result = new List<BrandViewModel>();

            var resultEntity = _brandRepository.GetBrandAll();

            foreach (var brand in resultEntity)
            {
                var entity = _mapper.Map<BrandViewModel>(brand);
                if (entity != null)
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        public void CreateBrand(CreateBrandInputModel inputModel)
        {
            if (inputModel != null && inputModel.id != 0)
            {
                var entity = _mapper.Map<BrandEntity>(inputModel);

                _brandRepository.UpdateBrand(entity);
            }
            else if (inputModel != null)
            {
                var entity = _mapper.Map<BrandEntity>(inputModel);

                entity.Active = true;

                _brandRepository.CreateBrand(entity);
            }
            else
            {
                throw new Exception("Objeto de input não pode ser nulo!");
            }
        }

        public void RemoveBrand(int brandId)
        {
            if (brandId > 0)
            {
               var statusRemove = _brandRepository.CheckStatusRemoveBrand(brandId);

                if (!statusRemove)
                {
                    _brandRepository.RemoveBrand(brandId);
                }
                else
                {
                    throw new Exception("Há produtos associados a essa marca!");
                }
            }            
            else
            {
                throw new Exception("Objeto de input não foi válido!");
            }
        }
    }
}
