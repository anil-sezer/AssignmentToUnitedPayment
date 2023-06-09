namespace UPay.Application.Services.LoginAsMerchant.Dto;

public class MerchantLoginResponse
{
    public bool Fail { get; set; }
    public int StatusCode { get; set; }
    public MerchantLoginResponseInnerModel Result { get; set; }
    public int Count { get; set; }
    public string ResponseCode { get; set; }
    public string ResponseMessage { get; set; }
}
