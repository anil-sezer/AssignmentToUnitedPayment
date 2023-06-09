using UPay.Application.Boilerplate;
using UPay.Application.Boilerplate.Dto;
using UPay.Application.Services.Buyer.Dto;
using UPay.Application.Services.IdentityVerification;
using UPay.DataAccess;
using UPay.Domain.Entities;

namespace UPay.Application.Services.Buyer;

public class BuyerAppService : AppServiceBase, IBuyerAppService
{
    private readonly UPayDbContext _dbContext;
    private readonly IIdentityVerificationAppService _identityVerification;

    public BuyerAppService(UPayDbContext dbContext, IIdentityVerificationAppService identityVerification)
    {
        _dbContext = dbContext;
        _identityVerification = identityVerification;
    }
    
    public async Task<AppServiceResult> AddCustomerAsync(AddBuyerInput input)
    {
        var isThisIdNumberIsVerified = await _identityVerification.ValidateTcknAsync(input.Name, input.Surname, input.BirthDate, input.IdentityNo);
        
        await _dbContext.Customer.AddAsync(new Customer
        {
            Name = input.Name,
            Surname = input.Surname,
            BirthDate = input.BirthDate,
            IdentityNo = input.IdentityNo,
            IdentityNoVerified = isThisIdNumberIsVerified,
            Status = "PLACEHOLDER"
        });
        await _dbContext.SaveChangesAsync();

        return Success();
    }
}
