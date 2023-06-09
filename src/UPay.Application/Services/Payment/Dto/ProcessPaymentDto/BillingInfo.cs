namespace UPay.Application.Services.Payment.Dto.ProcessPaymentDto;

public class BillingInfo
{
    public string TaxNo { get; set; }
    public string TaxOffice { get; set; }
    public string FirmName { get; set; }
    public string IdentityNumber { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Town { get; set; }
    public string ZipCode { get; set; }
}