using UPay.Application.Boilerplate.Dto;
using UPay.Application.Services.Buyer.Dto;

namespace UPay.Application.Services.Buyer;

public interface IBuyerAppService
{
    Task<AppServiceResult> AddCustomerAsync(AddBuyerInput input);
}
