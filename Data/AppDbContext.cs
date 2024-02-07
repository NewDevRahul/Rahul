using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RoshanDemo1.Models;
using System.Collections.Generic;
using test.Models;

namespace RoshanDemo1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<CountryModel> Country { get; set; }

        public DbSet<StateModel> States { get; set; }

        public DbSet<CityModel> Cities { get; set; }
    }
}
