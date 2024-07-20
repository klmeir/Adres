using Adres.Api.DataSource;
using Adres.Api.Services;
using Azure.Storage.Blobs;

namespace Adres.Api.Extensions
{
    public static class AutoLoadServices
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {            
            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));                        

            return services;
        }

        public static IServiceCollection AddStorageSupport(this IServiceCollection services, IConfiguration configuration)
        {
            var isEnabled = bool.Parse(configuration["BlobStorageSettings:Enabled"] ?? "false");
            if (isEnabled)
            {
                services.AddSingleton(x => new BlobServiceClient(configuration["BlobStorageSettings:ConnectionString"]));
                services.AddScoped<IFileStorageService, BlobStorageService>();
            }
            else
            {
                //services.AddScoped<IFileStorageService, FakeFileStorageService>();
            }

            return services;
        }
    }
}
