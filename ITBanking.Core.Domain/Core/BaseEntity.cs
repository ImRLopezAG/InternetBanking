namespace ITBanking.Core.Domain.Core;

public class BaseEntity {
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime LastModifiedAt { get; set; }
}
