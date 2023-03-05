using ITBanking.Core.Domain.Core;
using ITBanking.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITBanking.Infrastructure.Persistence.Context;

public class ITBankingContext : DbContext {
  public ITBankingContext(DbContextOptions<ITBankingContext> options) : base(options) { }
  public DbSet<User> Users { get; set; }

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
    modelBuilder.Entity<User>().ToTable("Users");
    #endregion
  }
}