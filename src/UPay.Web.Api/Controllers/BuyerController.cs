using Microsoft.AspNetCore.Mvc;
using UPay.Application.Services.Buyer;
using UPay.Application.Services.Buyer.Dto;
using UPay.Web.Api.Models;

namespace UPay.Web.Api.Controllers;

public class BuyerController : ApiControllerBase
{
    private readonly IBuyerAppService _appService;

    public BuyerController(IBuyerAppService appService)
    {
        _appService = appService;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResult>> Customer([FromBody] AddBuyerInput input)
    {
        var result = await _appService.AddCustomerAsync(input);
    
        return ApiResponse(result);
    }
}
