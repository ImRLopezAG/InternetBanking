using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITBanking.Infrastructure.Identity.Entities;

namespace ITBanking.Infrastructure.Identity.Interfaces;

public interface IRequestService
{
  Task<string> SendVerificationEmail(ApplicationUser user, string origin);
  Task<string> SendForgotPassword(ApplicationUser user, string origin);

}
