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
    public class PartnerBusiness : IPartnerBusiness
    {
        private readonly IPartnerRepository _partnerRepository;
        public readonly IMapper _mapper;

        public PartnerBusiness(IPartnerRepository partnerRepository, IMapper mapper)
        {
            _partnerRepository = partnerRepository;
            _mapper = mapper;
        }

        public List<ListAllPartnersViewModel> GetListPartnerAll(ListAllPartnersInputModel listAllPartnersInputModel)
        {
            if (listAllPartnersInputModel != null)
            {
                List<ListAllPartnersViewModel> resultList = new List<ListAllPartnersViewModel>();

                var resultEntity = _partnerRepository.GetPartnerAll(listAllPartnersInputModel.Id, listAllPartnersInputModel.CPF, listAllPartnersInputModel.CNPJ, listAllPartnersInputModel.Name);

                foreach (var partner in resultEntity)
                {
                    var entity = _mapper.Map<ListAllPartnersViewModel>(partner);
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

        public PartnerViewModel GetPartnerById(int id)
        {
            if (id != 0)
            {
                var resultEntity = _partnerRepository.GetPartnerById(id);

                var result = _mapper.Map<PartnerViewModel>(resultEntity);

                result.DateOfBirthFormat = resultEntity.DateOfBirth.ToString("yyyy-MM-dd");

                return result;
            }
            else
            {
                throw new Exception("Id de parceiro informado não é valido!");
            }
        }

        public void AlterOrCreatePartner(CreatePartnerInputModel inputModel)
        {
            if (inputModel != null && inputModel.id != 0)
            {
                var entity = _mapper.Map<PartnerEntity>(inputModel);

                _partnerRepository.UpdatePartner(entity);
            }
            else if (inputModel != null)
            {
                var entity = _mapper.Map<PartnerEntity>(inputModel);

                _partnerRepository.CreatePartner(entity);
            }
            else
            {
                throw new Exception("Objeto de input não pode ser nulo!");
            }
        }

        public List<PartnerResumeListViewModel> GetAllPartnersResumeList()
        {
            List<PartnerResumeListViewModel> resultList = new List<PartnerResumeListViewModel>();

            var resultEntity = _partnerRepository.GetAllPartnersResumeList();

            foreach (var partner in resultEntity)
            {
                var entity = _mapper.Map<PartnerResumeListViewModel>(partner);
                if (entity != null)
                {
                    resultList.Add(entity);
                }
            }

            return resultList;
        }
    }
}
