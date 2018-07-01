using Microsoft.EntityFrameworkCore;

namespace BankAccounts.Models
{
    public class BankAccountContext : DbContext
    {
        public BankAccountContext(DbContextOptions<BankAccountContext> options) : base(options) {}
        public DbSet<User> users {get; set;}
        public DbSet<Transaction> transactions {get; set;}
    }
}