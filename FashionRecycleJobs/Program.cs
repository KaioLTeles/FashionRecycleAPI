// See https://aka.ms/new-console-template for more information
using FashionRecycleJobs.Business;

Console.WriteLine("Iniciando Execussão!");

PaymentRecurringJobBusiness paymentRecurringJob = new PaymentRecurringJobBusiness();

paymentRecurringJob.StartProcess();


Console.WriteLine("Finalizado Execussão!");
