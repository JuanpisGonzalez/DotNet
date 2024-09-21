using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleDotNetMvcCrud.Models;

namespace SimpleDotNetMvcCrud.Data
{
    public class SimpleDotNetMvcCrudContext : DbContext
    {
        public SimpleDotNetMvcCrudContext (DbContextOptions<SimpleDotNetMvcCrudContext> options)
            : base(options)
        {
        }

        public DbSet<SimpleDotNetMvcCrud.Models.Person> Person { get; set; } = default!;
    }
}
