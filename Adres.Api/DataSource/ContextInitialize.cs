using Adres.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Adres.Api.DataSource
{
    public class ContextInitialize
    {
        protected ContextInitialize() { }

        public static void Seed(IServiceProvider serviceProvider)
        {
            // migrate the database.  Best practice = in Main, using service scope
            using (var scope = serviceProvider.CreateScope())
            {

                try
                {
                    var context = scope.ServiceProvider.GetService<DataContext>();

                    context.Database.Migrate();

                    // for testing purposes                    
                    SeedData(scope.ServiceProvider);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<ContextInitialize>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }

            }

        }

        private static void SeedData(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<ContextInitialize>>();
            var context = serviceProvider.GetService<DataContext>();
            var dataset = context.Set<Acquisition>();

            if (!dataset.Any()) 
            {
                dataset.Add(new Acquisition { 
                    Budget = 10000000,
                    Unit = "Dirección de Medicamentos y Tecnologías en Salud",
                    Type = "Medicamentos",
                    Quantity = 10000,
                    UnitValue = 1000,
                    TotalValue = 10000000,
                    AcquisitionDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7)),
                    Supplier = "Laboratorios Bayer S.A.",
                    Documentation = new List<DocumentationFile> {
                        new DocumentationFile { Name = "Prueba Técnica para Desarrollador - ADRES.pdf", Url = "https://nitroadres.blob.core.windows.net/nitro-public-uploads/af7fd8a2-5b71-4770-b1f8-5bec86fba573.pdf" }
                    }

                });
                _ = context.SaveChangesAsync().Result;
            }            
        }

    }
}
