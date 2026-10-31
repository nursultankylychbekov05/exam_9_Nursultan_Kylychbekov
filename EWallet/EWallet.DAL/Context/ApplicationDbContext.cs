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

        builder.Entity<ApplicationUser>()
            .HasIndex(u => u.AccountNumber)
            .IsUnique();

        builder.Entity<ProviderAccount>()
            .HasIndex(a => new { a.ProviderId, a.Identifier })
            .IsUnique();
    }
}