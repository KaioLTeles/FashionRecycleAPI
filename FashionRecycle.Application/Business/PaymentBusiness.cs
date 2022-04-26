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

        public PaymentBusiness(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public PaymentViewModel GetPaymentById(int idPayment)
        {
            if (idPayment != 0)
            {
                var resultEntity = _paymentRepository.GetPaymentById(idPayment);

                var result = _mapper.Map<PaymentViewModel>(resultEntity);

                return result;
            }
            else
            {
                throw new Exception("Id de Pagamento informado não é valido!");
            }
        }

        public List<PaymentViewModel> GetListPaymentAll(int idPayment, int idPartner, int idProvider)
        {

            List<PaymentViewModel> resultList = new List<PaymentViewModel>();

            var resultEntity = _paymentRepository.GetPaymentAll(idPayment, idProvider, idPartner);

            foreach (var provider in resultEntity)
            {
                var entity = _mapper.Map<PaymentViewModel>(provider);
                if (entity != null)
                {
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
    }
}
