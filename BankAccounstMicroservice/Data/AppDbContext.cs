using BankAccountsMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAccountsMicroservice.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Account> Cuentas { get; set; }

        public DbSet<Movement> Movimientos { get; set; }

    }
}