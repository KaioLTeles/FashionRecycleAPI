using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Interface
{
    public interface IProviderBusiness
    {
        ProviderViewModel GetProviderById(int idProvider);
        List<ListAllProvidersViewModel> GetListProviderAll(ListAllProvidersInputModel listAllProvidersInputModel);
        void AlterOrCreateProvider(CreateProviderInputModel inputModel);
        List<ProviderResumeListViewModel> GetAllProvidersResumeList();
    }
}
