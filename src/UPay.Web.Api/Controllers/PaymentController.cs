using Microsoft.AspNetCore.Mvc;
using UPay.Application.Services.Payment;
using UPay.Application.Services.Payment.Dto;
using UPay.Web.Api.Models;

namespace UPay.Web.Api.Controllers;

public class PaymentController : ApiControllerBase
{
    private readonly IPaymentAppService _appService;

    public PaymentController(IPaymentAppService appService)
    {
        _appService = appService;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResult>> Payment([FromBody] PaymentInput input)
    {
        var result = await _appService.ProcessPaymentAsync(input);
    
        return ApiResponse(result);
    }
}
