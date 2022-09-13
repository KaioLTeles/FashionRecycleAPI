using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.ViewModel
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public int LoginStatus { get; set; }
        public bool FirstLogin { get; set; }
        public int RoleUser { get; set; }
    }
}
