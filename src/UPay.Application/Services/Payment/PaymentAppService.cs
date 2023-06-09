using System.Security.Cryptography;
using System.Text;
using UPay.Application.Boilerplate;
using UPay.Application.Boilerplate.Dto;
using UPay.Application.Services.LoginAsMerchant;
using UPay.Application.Services.LoginAsMerchant.Dto;
using UPay.Application.Services.Payment.Dto;
using UPay.Application.Services.Payment.Dto.ProcessPaymentDto;
using UPay.DataAccess;
using UPay.Domain.Consts;

namespace UPay.Application.Services.Payment;

public class PaymentAppService : AppServiceBase, IPaymentAppService
{
    private readonly UPayDbContext _dbContext;
    private readonly ILoginAsMerchantAppService _appService;
    private readonly ThirdPartyApiCallHandler _thirdPartyApiCallHandler; 

    public PaymentAppService(UPayDbContext dbContext, ILoginAsMerchantAppService appService, ThirdPartyApiCallHandler thirdPartyApiCallHandler)
    {
        _dbContext = dbContext;
        _appService = appService;
        _thirdPartyApiCallHandler = thirdPartyApiCallHandler;
    }
    
    public async Task<AppServiceResult> ProcessPaymentAsync(PaymentInput input)
    {
        var merchantModel = await _appService.LoginAndGetModelAsync();
        
        var request = GenerateRequest(merchantModel, input);

        var headers = new Dictionary<string, string> { { "Authorization", "bearer " + merchantModel.Token } };

        var result = _thirdPartyApiCallHandler.HandleRestApiCallAsync<object>(request, HttpMethod.Post, ThirdPartyApiSettings.Url_NonSecurePayment, headers);

        return Success(); // todo: return something based on the result
    }

    private PaymentRequest GenerateRequest(MerchantModelFromJwt m, PaymentInput i)
    {
        var latestOrderId = _dbContext.Transaction.OrderBy(x => x.Id).LastOrDefault()?.OrderId ?? 0;
        var orderId = latestOrderId + 1;

        var rndCode = Guid.NewGuid().ToString(); // todo: Bu hatalı olabilir. Başka bir şey istiyor sanki?
        
        var r = new PaymentRequest
        {
            MemberId = Convert.ToInt32(m.MemberId),
            MerchantId = Convert.ToInt32(m.MerchantId),
            CustomerId = m.Email,
            CardNumber = i.CardNumber,
            ExpiryDateMonth = i.ExpiryDateMonth,
            ExpiryDateYear = i.ExpiryDateYear,
            Cvv = i.Cvv,
            UserCode = PaymentConsts.UserCode, // todo: Mailde iletilecekti diyor docs, göremiyorum
            TxnType = PaymentConsts.PayCode_Sale,
            InstallmentCount = "1",
            Currency = PaymentConsts.TryCurrencyCode,
            OrderId = orderId.ToString(),
            TotalAmount = i.TotalAmount,
            Rnd = rndCode,
            Hash = CreateHashForTheRequest(rndCode, orderId, i.TotalAmount, m.Email),
        };
        
        return r;
    } 
    
    private static string CreateHashForTheRequest(string rndCode, long orderId, string totalAmount, string customerId)
    {
        var hashString = $"{PaymentConsts.ApiKey}{PaymentConsts.UserCode}{rndCode}{PaymentConsts.PayCode_Sale}{totalAmount}{customerId}{orderId}";
        var s512 = SHA512.Create();
        var byteConverter = new UnicodeEncoding();
        var bytes = s512.ComputeHash(byteConverter.GetBytes(hashString));
        var hash = BitConverter.ToString(bytes).Replace("-","");
        return hash;
    }
}
