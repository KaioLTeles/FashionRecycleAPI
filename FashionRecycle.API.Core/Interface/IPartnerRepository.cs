using FashionRecycle.API.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Interface
{
    public interface IPartnerRepository
    {
        PartnerEntity GetPartnerById(int partnerId);
        List<PartnerEntity> GetPartnerAll(int partnerId, string cpf, string cnpj, string name);
        void CreatePartner(PartnerEntity partnerEntity);
        void UpdatePartner(PartnerEntity partnerEntity);
        List<PartnerEntity> GetAllPartnersResumeList();

    }
}
