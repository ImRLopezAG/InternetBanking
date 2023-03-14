using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITBanking.Core.Application.Contracts;
using ITBanking.Core.Application.Core;
using ITBanking.Core.Application.Interfaces;
using ITBanking.Core.Application.ViewModels;
using ITBanking.Core.Application.ViewModels.SaveVm;
using ITBanking.Core.Domain.Entities;


namespace ITBanking.Core.Application.Services;

public class PaymentService: GenericService<PaymentVm, PaymentSaveVm,Payment>, IPaymentService
{
  private readonly IPaymentRepository _paymentRepository;
  private readonly IMapper _mapper;

  public PaymentService(IPaymentRepository paymentRepository, IMapper mapper) :base (paymentRepository, mapper)
  {
    _paymentRepository = paymentRepository;
    _mapper = mapper;
  }
}