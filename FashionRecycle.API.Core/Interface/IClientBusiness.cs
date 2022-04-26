using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Interface
{
    public interface IClientBusiness
    {
        List<ListAllClientsViewModel> GetAllClientsList(ListAllClientsInputModel listAllClientsInputModel);
        ClientViewModel GetClientById(int idClient);
        void AlterOrCreateClient(CreateClientInputModel inputModel);
        List<ClientResumeListViewModel> GetClienAllResume();
    }
}
