using ITBanking.Core.Application.Contracts.Core;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Domain.Entities;

namespace ITBanking.Core.Application.Contracts;

public interface IPaymentService : IGenericService<PaymentVm, PaymentSaveVm, Payment> {
  Task<PaymentSaveVm> SaveAdvance(PaymentSaveVm model);
  Task<PaymentSaveVm> PayCreditCard(PaymentSaveVm model);
}
