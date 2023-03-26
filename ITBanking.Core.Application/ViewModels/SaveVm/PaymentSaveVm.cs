namespace ITBanking.Core.Application.ViewModels.SaveVm;

public class PaymentSaveVm : BaseTransferVM {

  public IEnumerable<ProductVm> Saving { get; set; } = null!;
  public IEnumerable<ProductVm> Credit { get; set; } = null!;
  public IEnumerable<ProductVm> Loans { get; set; } = null!;
  public IEnumerable<BeneficiaryVm> Beneficiaries { get; set; } = null!;
}
