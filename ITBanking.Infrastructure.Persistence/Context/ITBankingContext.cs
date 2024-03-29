using ITBanking.Core.Domain.Core;
using ITBanking.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITBanking.Infrastructure.Persistence.Context;

public class ITBankingContext : DbContext {
  public ITBankingContext(DbContextOptions<ITBankingContext> options) : base(options) { }

  public DbSet<Beneficiary> Beneficiaries { get; set; }
  public DbSet<Payment> Payments { get; set; }
  public DbSet<Transfer> Transfers { get; set; }
  public DbSet<Product> Products { get; set; }
  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new()) {
    foreach (var entry in ChangeTracker.Entries<BaseEntity>())
      switch (entry.State) {
        case EntityState.Added:
          entry.Entity.CreatedAt = DateTime.Now;
          break;
        case EntityState.Modified:
          entry.Entity.LastModifiedAt = DateTime.Now;
          break;
      }
    return base.SaveChangesAsync(cancellationToken);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    #region  Tables
    modelBuilder.Entity<Beneficiary>().ToTable("Beneficiaries");
    modelBuilder.Entity<Payment>().ToTable("Payments");
    modelBuilder.Entity<Transfer>().ToTable("Transfers");
    modelBuilder.Entity<Product>().ToTable("Products");

    #endregion

    #region Keys
    modelBuilder.Entity<Beneficiary>().HasKey(x => x.Id);
    modelBuilder.Entity<Payment>().HasKey(x => x.Id);
    modelBuilder.Entity<Transfer>().HasKey(x => x.Id);
    modelBuilder.Entity<Product>().HasKey(x => x.Id);
    #endregion

    #region Configuration
    modelBuilder.Entity<Beneficiary>().Property(x => x.Id).UseIdentityColumn(1, 1);
    modelBuilder.Entity<Payment>().Property(x => x.Id).UseIdentityColumn(1, 1);
    modelBuilder.Entity<Transfer>().Property(x => x.Id).UseIdentityColumn(1, 1);
    modelBuilder.Entity<Product>().Property(x => x.Id).UseIdentityColumn(1, 1);
    #endregion

    #region Relations
    modelBuilder.Entity<Beneficiary>()
      .HasOne(x => x.Product)
      .WithMany(x => x.Beneficiaries)
      .HasForeignKey(x => x.ProductId);

    modelBuilder.Entity<Payment>()
      .HasOne(x => x.RProduct)
      .WithMany(x => x.RPayments)
      .HasForeignKey(x => x.RProductId);

    modelBuilder.Entity<Transfer>()
      .HasOne(x => x.RProduct)
      .WithMany(x => x.RTransfers)
      .HasForeignKey(x => x.RProductId);


    modelBuilder.Entity<Payment>()
      .HasOne(x => x.SProduct)
      .WithMany(x => x.SPayments)
      .HasForeignKey(x => x.SProductId)
      .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<Transfer>()
      .HasOne(x => x.SProduct)
      .WithMany(x => x.STransfers)
      .HasForeignKey(x => x.SProductId)
      .OnDelete(DeleteBehavior.NoAction);



    #endregion

    #region Configuration

    modelBuilder.Entity<Product>().HasIndex(x => x.AccountNumber).IsUnique();

    #endregion

  }
}