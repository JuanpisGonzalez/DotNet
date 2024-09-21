using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetMvcInnerQuery.Models;

namespace DotNetMvcInnerQuery.Data
{
    public class DotNetMvcInnerQueryContext : DbContext
    {
        public DotNetMvcInnerQueryContext (DbContextOptions<DotNetMvcInnerQueryContext> options)
            : base(options)
        {
        }

        public DbSet<DotNetMvcInnerQuery.Models.Product> Products { get; set; } = default!;
        public DbSet<DotNetMvcInnerQuery.Models.Sale> Sales { get; set; } = default!;
        public DbSet<DotNetMvcInnerQuery.Models.Provider> Providers { get; set; } = default!;
        public DbSet<DotNetMvcInnerQuery.Models.Client> Clients { get; set; } = default!;
    }
}
