namespace UPay.Application.Services.Payment.Dto.ProcessPaymentDto;

public class OrderProduct
{
    public int MerchantId { get; set; }
    public string ProductId { get; set; }
    public string Amount { get; set; }
    public string ProductName { get; set; }
    public string CommissionRate { get; set; }
}