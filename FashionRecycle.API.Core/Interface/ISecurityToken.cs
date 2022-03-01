using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Interface
{
    public interface ISecurityToken
    {
        string GenerateJwtToken(string email, string role);
    }
}
