using Microsoft.AspNetCore.Mvc;

namespace UPay.Web.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ApiControllerBase : ControllerBase
    {
    }
}
