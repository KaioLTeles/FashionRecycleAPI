using FashionRecycle.API.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Interface
{
    public interface IRecievablesBusiness
    {
        List<RecievableEntity> GetReciavableAll(string inicialDate, string finalDate, int idClient);
        void CreateRecievableFromSale(SalesEntity salesEntity, int saleId);
        void UpdateReceiable(int idReceiable);
    }
}
