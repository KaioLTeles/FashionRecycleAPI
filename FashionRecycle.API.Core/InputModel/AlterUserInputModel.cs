using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.InputModel
{
    public class AlterUserInputModel
    {
        public int idUser { get; set; }
        public string? email { get; set; }
        public string? name { get; set; }
        public string? userName { get; set; }
        public string? password { get; set; }
        public int roleId { get; set; }
        public bool active { get; set; }
    }
}
