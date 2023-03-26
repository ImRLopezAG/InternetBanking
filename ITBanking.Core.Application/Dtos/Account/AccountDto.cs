namespace ITBanking.Core.Application.Dtos.Account;

public class AccountDto {
  public string Id { get; set; } = null!;
  public string UserName { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string FullName { get; set; } = null!;
  public string FirstName { get; set; } = null!;
  public string LastName { get; set; } = null!;
  public string DNI { get; set; } = null!;
  public bool EmailConfirmed { get; set; }
  public int Products { get; set; }
  public string Role { get; set; } = null!;
  public string PhoneNumber { get; set; } = null!;
}
