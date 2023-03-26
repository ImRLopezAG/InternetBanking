using ITBanking.Infrastructure.Identity.Entities;

namespace ITBanking.Infrastructure.Identity.Interfaces;

public interface IRequestService {
  Task<string> SendVerificationEmail(ApplicationUser user, string origin);
  Task<string> SendForgotPassword(ApplicationUser user, string origin);

}
