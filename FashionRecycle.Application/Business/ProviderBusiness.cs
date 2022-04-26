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
    public class ProviderBusiness : IProviderBusiness
    {
        private readonly IProviderRepository _providerRepository;
        public readonly IMapper _mapper;

        public ProviderBusiness(IProviderRepository providerRepository, IMapper mapper)
        {
            _providerRepository = providerRepository;
            _mapper = mapper;
        }

        public ProviderViewModel GetProviderById(int idProvider)
        {
            if (idProvider != 0)
            {
                var resultEntity = _providerRepository.GetProviderById(idProvider);

                var result = _mapper.Map<ProviderViewModel>(resultEntity);

                return result;
            }
            else
            {
                throw new Exception("Id de fornecedor informado não é valido!");
            }
        }

        public List<ListAllProvidersViewModel> GetListProviderAll(ListAllProvidersInputModel listAllProvidersInputModel)
        {
            if (listAllProvidersInputModel != null)
            {
                List<ListAllProvidersViewModel> resultList = new List<ListAllProvidersViewModel>();

                var resultEntity = _providerRepository.GetProviderAll(listAllProvidersInputModel.Id, listAllProvidersInputModel.CNPJ);

                foreach (var provider in resultEntity)
                {
                    var entity = _mapper.Map<ListAllProvidersViewModel>(provider);
                    if (entity != null)
                    {
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

        public void AlterOrCreateProvider(CreateProviderInputModel inputModel)
        {
            if (inputModel != null && inputModel.id != 0)
            {
                var entity = _mapper.Map<ProviderEntity>(inputModel);

                _providerRepository.UpdateProvider(entity);
            }
            else if (inputModel != null)
            {
                var entity = _mapper.Map<ProviderEntity>(inputModel);

                _providerRepository.CreateProvider(entity);
            }
            else
            {
                throw new Exception("Objeto de input não pode ser nulo!");
            }
        }

        public List<ProviderResumeListViewModel> GetAllProvidersResumeList()
        {
            List<ProviderResumeListViewModel> resultList = new List<ProviderResumeListViewModel>();

            var resultEntity = _providerRepository.GetAllProvidersResumeList();

            foreach (var provider in resultEntity)
            {
                var entity = _mapper.Map<ProviderResumeListViewModel>(provider);
                if (entity != null)
                {
                    resultList.Add(entity);
                }
            }

            return resultList;
        }
    }
}
