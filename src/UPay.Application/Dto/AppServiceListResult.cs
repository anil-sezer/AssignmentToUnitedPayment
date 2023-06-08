namespace UPay.Application.Dto;

public class AppServiceListResult<TListOutput> : AppServiceResult where TListOutput : class
{
    public List<TListOutput> Items { get; set; }

    public AppServiceListResult()
    {
            
    }
}
