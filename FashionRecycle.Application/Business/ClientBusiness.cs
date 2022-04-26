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
    public class ClientBusiness : IClientBusiness
    {
        private readonly IClientRepository _clientRepository;
        public readonly IMapper _mapper;        

        public ClientBusiness(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;            
        }

        public List<ListAllClientsViewModel> GetAllClientsList(ListAllClientsInputModel listAllClientsInputModel)
        {
            if(listAllClientsInputModel != null)
            {
                List<ListAllClientsViewModel> resultList = new List<ListAllClientsViewModel>();

                var resultEntity = _clientRepository.GetClienAll(listAllClientsInputModel.Id, listAllClientsInputModel.CPF, listAllClientsInputModel.Name);

                foreach(var client in resultEntity)
                {
                    var clientEntity = _mapper.Map<ListAllClientsViewModel>(client);
                    if(clientEntity != null)
                    {
                        resultList.Add(clientEntity);
                    }
                }

                return resultList;
            }
            else
            {
                throw new Exception("Objeto de input não pode ser nulo!");
            }
                        
        }

        public ClientViewModel GetClientById(int idClient)
        {
            if(idClient != 0)
            {
                var resultEntity = _clientRepository.GetClientById(idClient);

                var result = _mapper.Map<ClientViewModel>(resultEntity);

                return result;
            }
            else
            {
                throw new Exception("Id de cliente informado não é valido!");
            }
        }

        public void AlterOrCreateClient(CreateClientInputModel inputModel)
        {
            if(inputModel != null && inputModel.id != 0)
            {
                var entity = _mapper.Map<ClientEntity>(inputModel);

                _clientRepository.UpdateClient(entity);
            }
            else if(inputModel != null)
            {
                var entity = _mapper.Map<ClientEntity>(inputModel);

                _clientRepository.CreateClient(entity);
            }
            else
            {
                throw new Exception("Objeto de input não pode ser nulo!");
            }
        }

        public List<ClientResumeListViewModel> GetClienAllResume()
        {
            List<ClientResumeListViewModel> resultList = new List<ClientResumeListViewModel>();
            
            var resultEntuty = _clientRepository.GetClienAllResume();

            foreach (var client in resultEntuty)
            {
                var clientEntity = _mapper.Map<ClientResumeListViewModel>(client);
                if (clientEntity != null)
                {
                    resultList.Add(clientEntity);
                }
            }            

            return resultList;
        }
    }
}
