using System;
using auth_infra.Models;
using Microsoft.EntityFrameworkCore;

namespace auth_infra.Data
{
    public class AcountContext : DbContext
    {
        public AcountContext(DbContextOptions<AcountContext> options) : base(options)
        {
        }

        public DbSet<Acount> Acounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Acount>().ToTable("Acount");
        }
    }
}
