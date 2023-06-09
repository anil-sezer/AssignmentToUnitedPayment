using UPay.Application.Boilerplate.Dto;
using UPay.Application.Services.Payment.Dto;

namespace UPay.Application.Services.Payment;

public interface IPaymentAppService
{
    Task<AppServiceResult> ProcessPaymentAsync(PaymentInput input);
}
