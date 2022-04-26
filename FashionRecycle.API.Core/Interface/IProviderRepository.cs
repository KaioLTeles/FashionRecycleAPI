using FashionRecycle.API.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Interface
{
    public interface IProviderRepository
    {
        ProviderEntity GetProviderById(int providerId);
        List<ProviderEntity> GetProviderAll(int providerId, string cnpj);
        void CreateProvider(ProviderEntity providerEntity);
        void UpdateProvider(ProviderEntity providerEntity);
        List<ProviderEntity> GetAllProvidersResumeList();
    }
}
