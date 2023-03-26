using AutoMapper;
using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Core;
using ITBanking.Core.Application.Helpers;
using ITBanking.Core.Application.Interfaces;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Domain.Entities;


namespace ITBanking.Core.Application.Services;

public class PaymentService : GenericService<PaymentVm, PaymentSaveVm, Payment>, IPaymentService {
  private readonly IPaymentRepository _paymentRepository;
  private readonly IProductRepository _productRepository;
  private readonly IAccountService _userService;
  private readonly IMapper _mapper;

  public PaymentService(IPaymentRepository paymentRepository, IProductRepository productRepository, IAccountService userService, IMapper mapper) : base(paymentRepository, mapper) {
    _paymentRepository = paymentRepository;
    _productRepository = productRepository;
    _userService = userService;
    _mapper = mapper;
  }

  public override async Task<PaymentSaveVm> Save(PaymentSaveVm model) {
    var sender = await _productRepository.GetEntity(model.SProductId);
    var receptor = await _productRepository.GetEntity(model.RProductId);

    if (sender == null || receptor == null) {
      model.HasError = true;
      model.Error = $"{(sender == null ? "sender" : "receptor")} not found";
      return model;
    }
    var debt = receptor.Dbt * -1;

    var amount = model.Amount + (model.Amount * 0.0625);

    if (sender.Amount < amount || sender.Amount < debt) {
      model.HasError = true;
      model.Error = "Insufficient funds";
      return model;
    }

    var pay = amount > debt ? ( double )debt : amount;
    sender.Amount -= pay;
    receptor.Dbt += pay;

    Payment payment = new() {
      Amount = pay + (pay * 0.0625),
      Sender = sender.UserId,
      Receptor = receptor.UserId,
      RProductId = receptor.Id,
      SProductId = sender.Id,
    };

    try {
      await _paymentRepository.Save(payment);
      await _productRepository.Update(sender);
      await _productRepository.Update(receptor);
    } catch (Exception e) {
      model.HasError = true;
      model.Error = e.Message;
    }

    return model;
  }
  public override async Task<IEnumerable<PaymentVm>> GetAll() {
    var users = await _userService.GetAll();
    var products = await _productRepository.GetAll();

    var query = from payment in await _paymentRepository.GetAll()
                join user in users on payment.Sender equals user.Id
                join product in products on payment.RProductId equals product.Id
                select _mapper.Map<PaymentVm>(payment, opt => opt.AfterMap((src, pym) => {
                  pym.Name = user.FullName;
                  pym.Type = GetEnum.Products(product.TyAccountId);
                  pym.AccountNumber = product.AccountNumber;
                }));

    return query;
  }
  public async Task<PaymentSaveVm> SaveAdvance(PaymentSaveVm model) {
    var sender = await _productRepository.GetEntity(model.SProductId);
    var receptor = await _productRepository.GetEntity(model.RProductId);

    if (sender == null || receptor == null) {
      model.HasError = true;
      model.Error = $"{(sender == null ? "sender" : "receptor")} not found";
      return model;
    }
    var debt = sender.Dbt * -1;
    var amount = model.Amount + (model.Amount * 0.0625);
    var limit = sender.Limit;

    if (limit < amount || limit < debt || sender.Amount < model.Amount) {
      model.HasError = true;
      model.Error = "You can't exceed the limit";
      return model;
    }

    sender.Amount -= amount;
    receptor.Amount += amount;
    sender.Dbt += amount * -1;

    Payment payment = new() {
      Amount = amount,
      Sender = sender.UserId,
      Receptor = receptor.UserId,
      RProductId = receptor.Id,
      SProductId = sender.Id,
    };

    try {
      await _paymentRepository.Save(payment);
      await _productRepository.Update(sender);
      await _productRepository.Update(receptor);
    } catch (Exception e) {
      model.HasError = true;
      model.Error = e.Message;
    }

    return model;
  }
  public async Task<PaymentSaveVm> PayCreditCard(PaymentSaveVm model) {
    var sender = await _productRepository.GetEntity(model.SProductId);
    var receptor = await _productRepository.GetEntity(model.RProductId);

    if (sender == null || receptor == null) {
      model.HasError = true;
      model.Error = $"{(sender == null ? "sender" : "receptor")} not found";
      return model;
    }
    var debt = receptor.Dbt * -1;
    var amount = model.Amount + (model.Amount * 0.0625);


    double pay = amount > debt ? ( double )debt : amount;
    sender.Amount -= pay + (pay * 0.0625);
    receptor.Amount += pay;
    receptor.Dbt += pay;

    if (receptor.Dbt > 0) {
      receptor.Dbt = 0;
    }

    if (receptor.Amount > receptor.Limit) {
      receptor.Amount = ( double )(( double )receptor.Limit - (receptor.Limit * 0.0625));
    }

    Payment payment = new() {
      Amount = pay + (pay * 0.0625),
      Sender = sender.UserId,
      Receptor = receptor.UserId,
      RProductId = receptor.Id,
      SProductId = sender.Id,
    };

    try {
      await _paymentRepository.Save(payment);
      await _productRepository.Update(sender);
      await _productRepository.Update(receptor);
    } catch (Exception e) {
      model.HasError = true;
      model.Error = e.Message;
    }

    return model;
  }
}