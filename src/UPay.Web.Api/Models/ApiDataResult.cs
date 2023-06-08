namespace UPay.Web.Api.Models;

public class ApiDataResult<TData> : ApiResult
{
    public TData Data { get; set; }
}