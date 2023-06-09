namespace UPay.Application.Services.IdentityVerification;

public interface IIdentityVerificationAppService
{
    Task<bool> ValidateTcknAsync(string name, string surname, DateTime birthday, long tckn);
}
