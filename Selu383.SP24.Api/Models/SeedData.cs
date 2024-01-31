using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;


namespace Selu383.SP24.Api.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new DataContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<DataContext>>()))
        {
            // Look for any movies.
            if (context.Hotel.Any())
            {
                return;   // DB has been seeded
            }
            context.Hotel.AddRange(
                new Hotel
                {
                    Name = "Log Cabin Hotel",
                    Address = "65478 Tree Road",
                },
                 new Hotel
                 {
                     Name = "Hollywood Hotel",
                     Address = "1234 Hollywood Blvd.",
                 },
                  new Hotel
                  {
                      Name = "Huge Hotel",
                      Address = "57690 Ginormous Lane",
                  }

            );
            context.SaveChanges();
        }
    }

}

