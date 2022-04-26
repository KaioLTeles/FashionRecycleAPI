using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.ViewModel
{
    public class ProviderResumeListViewModel
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? LegalCompanyName { get; set; }
        public string? CNPJ { get; set; }
    }
}
