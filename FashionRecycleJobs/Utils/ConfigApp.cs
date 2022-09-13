using Microsoft.Extensions.Configuration;

namespace FashionRecycleJobs.Utils
{
    public class ConfigApp
    {
        public  IConfiguration ConfigConection()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();

            return config;
        }
    }
}
