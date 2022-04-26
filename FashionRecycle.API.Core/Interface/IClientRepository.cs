using FashionRecycle.API.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Interface
{
    public interface IClientRepository
    {
        ClientEntity GetClientById(int clientId);
        List<ClientEntity> GetClienAll(int clientId, string cpf, string name);
        void CreateClient(ClientEntity clientEntity);
        void UpdateClient(ClientEntity clientEntity);
        List<ClientEntity> GetClienAllResume();

    }
}
