using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.ViewModel
{
    public class ReportAllSalesResumedViewModel
    {
        public int Id { get; set; }
        public string? NameClient { get; set; }
        public double AmountSale { get; set; }
        public string? PaymentMethod { get; set; }
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
        public string? CreationDateFormated { get; set; }
        public string? ProductDesciption { get; set; }
        public string? AlternativeId { get; set; }
        public int NumberInstallments { get; set; }
    }
}
