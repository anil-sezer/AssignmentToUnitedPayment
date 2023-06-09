using System.ComponentModel.DataAnnotations;
using UPay.Domain.Enums;

namespace UPay.Domain.Entities;

public class Transaction : EntityBase
{
    public Guid CustomerId { get; set; }
    public long OrderId { get; set; }
    public TransactionType TypeId { get; set; }
    public decimal Amount { get; set; }
    [CreditCard] // todo: Easier than adding FluentValidation or regex
    public string CardPan { get; set; }
    public string ResponseCode { get; set; }
    public string ResponseMessage { get; set; }
    public string Status { get; set; }
}
