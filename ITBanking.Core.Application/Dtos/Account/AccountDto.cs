using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITBanking.Core.Application.Dtos.Account;

public class AccountDto{
  public  string Id { get; set; } = null!;
  public string UserName { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string FullName { get; set; } = null!;
  public string DNI { get; set; } = null!;
  public bool EmailConfirmed { get; set; }
}
