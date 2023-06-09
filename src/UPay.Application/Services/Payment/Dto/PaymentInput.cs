using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UPay.Domain.Consts;

namespace UPay.Application.Services.Payment.Dto;

public class PaymentInput
{
    [Required(ErrorMessage = CommonMessageConsts.Annotation_RequiredError)]
    [CreditCard(ErrorMessage = "Invalid credit card number.")]
    [DefaultValue("4355084355084358")] // todo: test vaule, remove later
    public string CardNumber { get; set; }
    
    [Required(ErrorMessage = CommonMessageConsts.Annotation_RequiredError)]
    [StringLength(2, ErrorMessage = "{0} must be exactly 2 characters.")]
    [DefaultValue("12")] // todo: test vaule, remove later
    public string ExpiryDateMonth { get; set; }
    
    [Required(ErrorMessage = CommonMessageConsts.Annotation_RequiredError)]
    [StringLength(2, ErrorMessage = "{0} must be exactly 2 characters.")]
    [DefaultValue("26")] // todo: test vaule, remove later
    public string ExpiryDateYear { get; set; }
    
    [Required(ErrorMessage = CommonMessageConsts.Annotation_RequiredError)]
    [StringLength(3, ErrorMessage = "{0} must be exactly 2 characters.")]
    [DefaultValue("000")] // todo: test vaule, remove later
    public string Cvv { get; set; }
    
    [Required(ErrorMessage = CommonMessageConsts.Annotation_RequiredError)]
    [DefaultValue("100")] // todo: test vaule, remove later
    public string TotalAmount { get; set; }
}
