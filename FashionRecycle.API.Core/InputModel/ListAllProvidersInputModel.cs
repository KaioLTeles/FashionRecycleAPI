using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.InputModel
{
    public class ListAllProvidersInputModel
    {
        public int Id { get; set; }
        public string? CNPJ { get; set; }
    }
}
