namespace UPay.Application.Boilerplate.Dto;

public class AppServiceDataResult<TOutput> : AppServiceResult where TOutput : class
{
    public TOutput Data { get; set; }

    public AppServiceDataResult()
    {
    }
}
