using Microsoft.EntityFrameworkCore;
using ApplicationCars.Data;

namespace ApplicationCars.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationCarsContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ApplicationCarsContext>>()))
        {
            if (context == null || context.Car == null)
            {
                throw new ArgumentNullException("Null ApplicationCarsContext");
            }

            // Look for any movies.
            if (context.Car.Any())
            {
                return;   // DB has been seeded
            }

            context.Car.AddRange(
                new Car
                {
                    Brand = "Volskwagen",
                    Model = "Golf",
                    Price = 70000,
                    ProductionYear = DateTime.Parse("2020-01-01")
                },

                new Car
                {
                    Brand = "Skoda",
                    Model = "Octavia",
                    Price = 50000,
                    ProductionYear = DateTime.Parse("2019/02/02")
                },

                new Car
                {
                    Brand = "Porsche",
                    Model = "911",
                    Price = 250000,
                    ProductionYear = DateTime.Parse("2023-01-01")
                },

                new Car
                {
                    Brand = "Volvo",
                    Model = "XC90",
                    Price = 150000,
                    ProductionYear = DateTime.Parse("2022-01-01")
                }
            );
            context.SaveChanges();
        }
    }
}