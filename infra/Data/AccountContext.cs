using System;
using auth_infra.Models;
using Microsoft.EntityFrameworkCore;

namespace auth_infra.Data
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
        }
    }
}
