namespace UPay.Application.Services.LoginAsMerchant.Dto;

public class MerchantModelFromJwt
{
    public string Token { get; set; }
    public string Lang { get; set; }
    public string Email { get; set; }
    public string UserId { get; set; }
    public string MemberId { get; set; }
    public string MemberCode { get; set; }
    public string MerchantId { get; set; }
    public string MerchantNumber { get; set; }
    public string UserStatus { get; set; }
    public string ChangePasswordRequired { get; set; }
    public string PasswordStatus { get; set; }
    public string UserRoles { get; set; }
    public string RoleScore { get; set; }
    public string TicketType { get; set; }
    public string UserType { get; set; }
    public long nbf { get; set; }
    public long exp { get; set; }
    public long iat { get; set; }
}