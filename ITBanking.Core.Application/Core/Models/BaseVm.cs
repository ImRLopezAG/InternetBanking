namespace ITBanking.Core.Application.Core.Models;

public class BaseVm {
  public int Id { get; set; }
  public bool HasError { get; set; }
  public string? Error { get; set; }= null!;
  public DateTime CreatedAt { get; set; }
  public DateTime LastModifiedAt { get; set; }
}
