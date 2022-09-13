using FashionRecycle.API.Core.Entity;
using FashionRecycle.API.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.Application.Business
{
    public class RecievablesBusiness : IRecievablesBusiness
    {
        private readonly IRecievablesRepository _recievablesRepository;
        private readonly IClientBusiness _clientBusiness;

        public RecievablesBusiness(IRecievablesRepository recievablesRepository, IClientBusiness clientBusiness)
        {
            _recievablesRepository = recievablesRepository;
            _clientBusiness = clientBusiness;
        }

        public List<RecievableEntity> GetReciavableAll(string inicialDate, string finalDate, int idClient)
        {
            return _recievablesRepository.GetReciavableAll(inicialDate, finalDate, idClient);
        }

        public void CreateRecievableFromSale(SalesEntity salesEntity, int saleId)
        {
            var client = _clientBusiness.GetClientById(salesEntity.IdClient);

            int paymentMethod = salesEntity.IdPaymentMethod;

            DateTime keepDate = DateTime.Now;

            if (paymentMethod == 4 || paymentMethod == 5)
            {
                for (int i = 1; i <= salesEntity.NumberInstallments; i++)
                {


                    if (i == 1)
                    {
                        RecievableEntity entity = new RecievableEntity
                        {
                            Name = "Recebimento da venda " + saleId + " do Comprador " + client.Name,
                            Amout = salesEntity.AmountSale / salesEntity.NumberInstallments,
                            Active = true,
                            ClientName = client.Name,
                            IdClient = salesEntity.IdClient,
                            CreationDate = DateTime.Now,
                            RecieveDate = DateTime.Now,
                            SaleDate = DateTime.Now,
                            Status = false,
                            IdSale = saleId

                        };

                        _recievablesRepository.CreateRecievable(entity);
                    }
                    else
                    {
                        keepDate = keepDate.AddDays(30);


                        RecievableEntity entity = new RecievableEntity
                        {
                            Name = "Recebimento da venda " + saleId + " do Comprador " + client.Name,
                            Amout = salesEntity.AmountSale / salesEntity.NumberInstallments,
                            Active = true,
                            ClientName = client.Name,
                            IdClient = salesEntity.IdClient,
                            CreationDate = DateTime.Now,
                            RecieveDate = keepDate,
                            SaleDate = DateTime.Now,
                            Status = false,
                            IdSale = saleId

                        };

                        _recievablesRepository.CreateRecievable(entity);
                    }
                }
            }
            else
            {
                RecievableEntity entity = new RecievableEntity
                {
                    Name = "Recebimento da venda " + saleId + " do Comprador " + client.Name,
                    Amout = salesEntity.AmountSale,
                    Active = true,
                    ClientName = client.Name,
                    IdClient = salesEntity.IdClient,
                    CreationDate = DateTime.Now,
                    RecieveDate = DateTime.Now,
                    SaleDate = DateTime.Now,
                    Status = false,
                    IdSale = saleId

                };

                _recievablesRepository.CreateRecievable(entity);
            }
        }

        public void UpdateReceiable(int idReceiable)
        {
            var receiable = _recievablesRepository.GetReciavableById(idReceiable);

            if (receiable != null)
            {
                if (receiable.Status)
                {
                    _recievablesRepository.UpdateReceivableToUnpaid(idReceiable);
                }
                else
                {
                    _recievablesRepository.UpdateReceivableToPaid(idReceiable);
                }
            }
            else
            {
                throw new Exception("Objeto de recebimento não encontrado!");
            }
        }

    }
}
