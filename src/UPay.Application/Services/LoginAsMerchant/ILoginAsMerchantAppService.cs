using UPay.Application.Services.LoginAsMerchant.Dto;

namespace UPay.Application.Services.LoginAsMerchant;

public interface ILoginAsMerchantAppService
{
    Task<MerchantModelFromJwt> LoginAndGetModelAsync();
}
