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

    public async Task<PaymentSaveVm> PayCreditCard(PaymentSaveVm model) {
        var sender = await _productRepository.GetEntity(model.SProductId);
        var receptor = await _productRepository.GetEntity(model.RProductId);

        if (sender == null || receptor == null) {
            model.HasError = true;
            model.Error = $"{(sender == null ? "sender" : "receptor")} not found";
            return model;
        }
        var debt = sender.Dbt * -1;
        //Esto debitaba en vez de acreditar en loans
        var amount = model.Amount + (model.Amount * 0.0625);

        if (sender.Amount < amount) {
            model.HasError = true;
            model.Error = "Insufficient funds";
            return model;
        }

        var pay = amount < debt ? ( double )debt : amount;
        sender.Amount -= model.Amount + (model.Amount * 0.0625);
        receptor.Dbt += model.Amount;
        receptor.Amount = model.Amount;

        Payment payment = new() {
            Amount = pay,
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

    public async Task<PaymentSaveVm> SaveAdvance(PaymentSaveVm model) {
        var sender = await _productRepository.GetEntity(model.SProductId);
        var receptor = await _productRepository.GetEntity(model.RProductId);

        if (sender == null || receptor == null) {
            model.HasError = true;
            model.Error = $"{(sender == null ? "sender" : "receptor")} not found";
            return model;
        }
        var debt = sender.Dbt * -1;
        //Esto debitaba en vez de acreditar en loans
        var amount = model.Amount + (model.Amount * 0.0625);

        if (sender.Limit < amount || sender.Limit < debt) {
            model.HasError = true;
            model.Error = "You can't exceed the limit";
            return model;
        }

        var pay = amount < debt ? ( double )debt : amount;
        sender.Dbt += model.Amount + (model.Amount * 0.0625) * -1;
        receptor.Amount += model.Amount;
        sender.Amount -= model.Amount + (model.Amount * 0.0625);

        Payment payment = new() {
            Amount = pay,
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
        sender.Amount -= model.Amount + (model.Amount * 0.0625);
        receptor.Dbt += pay;

        Payment payment = new() {
            Amount = pay,
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
}