using FashionRecycle.API.Core.Entity;
using FashionRecycleJobs.Infrastructure.Repository;
using FashionRecycleJobs.Utils;

namespace FashionRecycleJobs.Business
{
    public class PaymentRecurringJobBusiness
    {
        public void StartProcess()
        {
            ConfigApp configApp = new ConfigApp();

            var config = configApp.ConfigConection();

            try
            {
                if (ValidadeRowJob())
                {
                    Console.WriteLine("Buscando lista de pagamentos recorrentes");

                    PaymentRepository repository = new PaymentRepository(config);

                    var payments = repository.GetPaymentAllActive();

                    if (payments.Count > 0)
                    {
                        Console.WriteLine("Encontrado " + payments.Count + " pagamentos recorrentes");

                        foreach (var payment in payments)
                        {
                            PaymentsEntity entity = new PaymentsEntity
                            {
                                Name = payment.Name,
                                Amount = payment.Amount,
                                PaymentMade = false,
                                PaymentDate = payment.PaymentDate.AddDays(30),
                                Active = true,
                                PaymenyType = payment.PaymenyType,
                                RecurringPayment = true,
                                CreationDate = DateTime.Now
                            };

                            Console.WriteLine("Criando pagamento " + payment.Name + " para o mes que vem");

                            repository.CreatePayment(entity);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Não há pagamento marcados como recorrentes!");
                    }
                }
                else
                {
                    Console.WriteLine("Processo abortado não estamos no primeiro dia do mes!");
                }
                
            }
            catch(Exception e)
            {
                Console.WriteLine("Erro! - " + e.Message);
            }
        }

        private bool ValidadeRowJob()
        {
            DateTime date = DateTime.Now;
            if(date.Day == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
