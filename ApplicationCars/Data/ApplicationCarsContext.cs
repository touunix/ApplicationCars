using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApplicationCars.Models;

namespace ApplicationCars.Data
{
    public class ApplicationCarsContext : DbContext
    {
        public ApplicationCarsContext (DbContextOptions<ApplicationCarsContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationCars.Models.Car> Car { get; set; } = default!;
    }
}
