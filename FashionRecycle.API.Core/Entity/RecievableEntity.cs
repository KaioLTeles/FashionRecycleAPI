using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Entity
{
    public class RecievableEntity : BaseEntity
    {
        public string? Name { get; set; }
        public int IdClient { get; set; }
        public int IdSale { get; set; }
        public double Amout { get; set; }
        public DateTime SaleDate { get; set; }
        public DateTime RecieveDate { get; set; }
        public bool Status { get; set; }
        public bool Active { get; set; }
        public string? ClientName { get; set; }
        public string? SaleDateFormated { get; set; }
        public string? RecieveDateFormated { get; set; }
    }
}
