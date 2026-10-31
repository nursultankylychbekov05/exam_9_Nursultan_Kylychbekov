using EWallet.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EWallet.DAL.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Provider> Providers => Set<Provider>();
    public DbSet<ProviderAccount> ProviderAccounts => Set<ProviderAccount>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(e =>
        {
            e.HasIndex(u => u.AccountNumber).IsUnique();
            e.Property(u => u.Balance).HasPrecision(18, 2);
        });

        builder.Entity<Transaction>()
            .Property(t => t.Amount)
            .HasPrecision(18, 2);

        builder.Entity<ProviderAccount>(e =>
        {
            e.HasIndex(a => new { a.ProviderId, a.Identifier }).IsUnique();
            e.Property(a => a.Balance).HasPrecision(18, 2);
        });
    }
}