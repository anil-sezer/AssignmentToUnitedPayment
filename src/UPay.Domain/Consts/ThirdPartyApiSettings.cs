// ReSharper disable InconsistentNaming
namespace UPay.Domain.Consts;

public class ThirdPartyApiSettings
{
    // Links
    public const string Url_TcknVerification = "https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx";
    public const string Url_MerchantAuthentication = "https://ppgsecurity-test.birlesikodeme.com:55002/api/ppg/Securities/authenticationMerchant";
    public const string Url_NonSecurePayment = "https://ppgpayment-test.birlesikodeme.com:20000/api/ppg/Payment/NoneSecurePayment";
    
    // Rules
    public const int Rule_MaxRetries = 5;
    public const int Rule_DelayBetweenRetriesAsMilliSeconds = 1000;
}