using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.InputModel
{
    public class ReportSalesInputModel
    {
        public int idSale { get; set; }
        public int idClient { get; set; }        
        public int idPartner { get; set; }
        public int brand { get; set; }
        public string? inicialFilterDate { get; set; }
        public string? finalFilterDate { get; set; }
    }
}
