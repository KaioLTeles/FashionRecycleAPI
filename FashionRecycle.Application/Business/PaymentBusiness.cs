using AutoMapper;
using FashionRecycle.API.Core.Entity;
using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.Interface;
using FashionRecycle.API.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.Application.Business
{
    public class PaymentBusiness : IPaymentBusiness
    {
        private readonly IPaymentRepository _paymentRepository;
        public readonly IMapper _mapper;
        private readonly IPartnerBusiness _partnerBusiness;
        private readonly IProductBusiness _productBusiness;

        public PaymentBusiness(IPaymentRepository paymentRepository, IMapper mapper, IPartnerBusiness partnerBusiness, IProductBusiness productBusiness)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _partnerBusiness = partnerBusiness;
            _productBusiness = productBusiness;
            _productBusiness = productBusiness;
        }

        public PaymentViewModel GetPaymentById(int idPayment)
        {
            if (idPayment != 0)
            {
                var resultEntity = _paymentRepository.GetPaymentById(idPayment);

                var result = _mapper.Map<PaymentViewModel>(resultEntity);

                result.PaymentDateFormated = result.PaymentDate.ToString("yyyy-MM-dd");

                return result;
            }
            else
            {
                throw new Exception("Id de Pagamento informado não é valido!");
            }
        }

        public List<PaymentViewModel> GetListPaymentAll(string inicialDate, string finalDate)
        {

            List<PaymentViewModel> resultList = new List<PaymentViewModel>();

            var resultEntity = _paymentRepository.GetPaymentAll(inicialDate, finalDate);

            foreach (var provider in resultEntity)
            {
                var entity = _mapper.Map<PaymentViewModel>(provider);
                if (entity != null)
                {
                    entity.PaymentDateFormated = entity.PaymentDate.ToString("dd/MM/yyy");
                    resultList.Add(entity);
                }
            }

            return resultList;
        }

        public void AlterOrCreatePayment(CreatePaymentInputModel inputModel)
        {
            if (inputModel != null && inputModel.id != 0)
            {
                var entity = _mapper.Map<PaymentsEntity>(inputModel);

                _paymentRepository.UpdatePayment(entity);
            }
            else if (inputModel != null)
            {
                var entity = _mapper.Map<PaymentsEntity>(inputModel);

                _paymentRepository.CreatePayment(entity);
            }
            else
            {
                throw new Exception("Objeto de input não pode ser nulo!");
            }
        }

        public double GetMargin()
        {
            return _paymentRepository.GetMargin();
        }

        public void DeletePayment(int idPayment)
        {
            if(idPayment > 0)
            {
                _paymentRepository.DeletePayment(idPayment);
            }
            else
            {
                throw new Exception("Objeto de input não pode ser 0!");
            }
            
        }

        public void CreatePaymentPartner(List<SalesItemsEntity> salesItemsEntity)
        {
            foreach(var item in salesItemsEntity)
            {

                var partner = _partnerBusiness.GetPartnerById(item.IdPartner);

                var product = _productBusiness.GetProductById(item.IdProduct);

                PaymentsEntity entity = new PaymentsEntity
                {
                    PaymenyType = new PaymenyTypeEntity { Id = 12 },
                    PaymentDate = DateTime.Now.AddDays(7),
                    Active = true,
                    Name = "Pagamento de Comissão De Fornecedor - " + partner.Name + " - Produto " + product.AlternativeId + " - " + product.Name + " " + product.Model,
                    Amount = product.PricePartner
                };

                _paymentRepository.CreatePayment(entity);
            }
        }

    }
}
