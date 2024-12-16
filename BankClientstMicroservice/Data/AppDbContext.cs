using BankClientsMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace BankClientsMicroservice.Data {
    public class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) { }
       
        public DbSet<Client> Clientes { get; set; }

    }
}