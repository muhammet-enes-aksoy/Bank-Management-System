using Microsoft.EntityFrameworkCore;
using Vb.Data.Entity;

namespace Vb.Data;

public class VbDbContext : DbContext
{
    public VbDbContext(DbContextOptions<VbDbContext> options): base(options)
    {
    
    }   
    
    // dbset 
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountTransaction> AccountTransactions { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<EftTransaction> EftTransactions { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new AccountTransactionConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new EftTransactionConfiguration());
        modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
        base.OnModelCreating(modelBuilder);
    }
    
}