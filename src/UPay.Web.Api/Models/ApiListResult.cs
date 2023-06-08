namespace UPay.Web.Api.Models;

public class ApiListResult<TList> : ApiResult
{
    public List<TList> Items { get; set; }
}