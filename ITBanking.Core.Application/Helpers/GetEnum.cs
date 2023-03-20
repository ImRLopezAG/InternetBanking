using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Core.Application.Enums;

namespace ITBanking.Core.Application.Helpers;

public static class GetEnum{
  public static string Cards(int id) => ((CardType)id).ToString();
  public static string Products(int id) => ((ProductType)id).ToString();
  public static string Account(int id) => ((AccountType)id).ToString();
  public static string Role(int id) => ((Roles)id).ToString();
}
