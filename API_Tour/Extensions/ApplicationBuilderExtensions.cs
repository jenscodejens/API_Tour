using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament_Core.Entities;
using Tournament_Data.Data;

namespace Tournament_Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<TourDbContext>();

                try
                {
                    await SeedData.InitAsync(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }

            }
        }
    }
}
