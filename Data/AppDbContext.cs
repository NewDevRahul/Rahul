using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RoshanDemo1.Models;
using System.Collections.Generic;

namespace RoshanDemo1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<StateModel> States { get; set; }

        public DbSet<CityModel> Cities { get; set; }

        public DbSet<QuestionModel> Questions { get; set; }

        public DbSet<AnswerModel> Answers { get; set; }

        public DbSet<OptionModel> Options { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OptionModel>()
                 .HasOne(o => o.Question)
                 .WithMany(q => q.Options)
                 .HasForeignKey(o => o.QuestionId);
           
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(CoreEventId.NavigationBaseIncludeIgnored));
        }

    }
}
