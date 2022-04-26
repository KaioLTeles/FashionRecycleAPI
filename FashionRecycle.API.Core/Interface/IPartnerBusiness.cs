using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Interface
{
    public interface IPartnerBusiness
    {
        List<ListAllPartnersViewModel> GetListPartnerAll(ListAllPartnersInputModel listAllPartnersInputModel);
        PartnerViewModel GetPartnerById(int id);
        void AlterOrCreatePartner(CreatePartnerInputModel inputModel);
        List<PartnerResumeListViewModel> GetAllPartnersResumeList();
    }
}
