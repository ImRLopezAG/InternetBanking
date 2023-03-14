using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITBanking.Core.Application.Dtos;

public class BaseDto{
  public int Id { get; set; }
  public string Name { get; set; }= null!;
}
