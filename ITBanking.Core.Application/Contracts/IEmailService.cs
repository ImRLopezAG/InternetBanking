using ITBanking.Core.Application.Dtos;

namespace ITBanking.Core.Application.Contracts;

public interface IEmailService {
  Task SendEmail(EmailRequest request);
}
