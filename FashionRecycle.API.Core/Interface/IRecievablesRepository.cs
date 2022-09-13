using FashionRecycle.API.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Interface
{
    public interface IRecievablesRepository
    {
        List<RecievableEntity> GetReciavableAll(string inicialDate, string finalDate, int idClient);
        void CreateRecievable(RecievableEntity recievableEntity);
        void UpdateReceivableToPaid(int idReceivable);
        void UpdateReceivableToUnpaid(int idReceivable);
        RecievableEntity GetReciavableById(int idReceiable);
    }
}
