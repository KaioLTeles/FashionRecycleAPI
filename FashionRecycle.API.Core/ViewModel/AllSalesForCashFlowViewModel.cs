using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.ViewModel
{
    public class AllSalesForCashFlowViewModel
    {
        public DateTime SaleDate { get; set; }
        public string SaleDateText { get; set; }
        public double AmountSale { get; set; }
    }
}
