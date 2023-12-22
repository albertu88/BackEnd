using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Infrastructure.Models.Model
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<LoginModel>? LoginModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginModel>().HasData(new LoginModel
            {
                Id = 1,
                UserName = "albertico",
                Password = "albertico"
            });
        }
    }
}
