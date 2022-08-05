using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.ViewModel
{
    public class CashFlowReportViewModel
    {
        public DateTime RowDate { get; set; }
        public string RowDateText { get; set; }
        public double ValueRevenue { get; set; }
        public double ValueExpense { get; set; }
        public double Balance { get; set; }
    }
}
