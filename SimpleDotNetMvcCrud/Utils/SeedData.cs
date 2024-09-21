using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleDotNetMvcCrud.Data;
using SimpleDotNetMvcCrud.Models;
using System;
using System.Linq;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new SimpleDotNetMvcCrudContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<SimpleDotNetMvcCrudContext>>()))
        {
            // Look for any movies.
            if (context.Person.Any())
            {
                return;   // DB has been seeded
            }
            context.Person.AddRange(
                new Person
                {
                    Name = "Juan González",
                    Age = 25
                },
                new Person
                {
                    Name = "Gabriela Vargas",
                    Age = 24
                },
                new Person
                {
                    Name = "Camilo González",
                    Age = 25
                }
            );
            context.SaveChanges();
        }
    }
}