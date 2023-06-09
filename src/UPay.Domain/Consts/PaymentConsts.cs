// ReSharper disable InconsistentNaming
namespace UPay.Domain.Consts;

public class PaymentConsts
{
    public const string TryCurrencyCode = "949";
    public const string ApiKey = "kU8@iP3@";
    
    // UserCode olabilecek değerler şunlar:
    // UserId: 441
    // MemberCode: BO
    // MerchantId: 215
    // MerchantNumber: 215
    public const string UserCode = "test";
    
    // txnType's:
    public const string PayCode_Sale         = "Auth";
    public const string PayCode_PreProvision = "PreAuth";
    public const string PayCode_EndProvision = "PostAuth";
    public const string PayCode_CheckPayment = "Inquiry";
    public const string PayCode_PayWithQr    = "CheckPayment";
    public const string PayCode_PayPoint     = "PointAuth";
}