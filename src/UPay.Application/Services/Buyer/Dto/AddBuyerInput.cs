namespace UPay.Application.Services.Buyer.Dto;

public class AddBuyerInput
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
    public long IdentityNo { get; set; }
}
