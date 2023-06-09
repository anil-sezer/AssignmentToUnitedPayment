using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UPay.Domain.Consts;

namespace UPay.Application.Services.Buyer.Dto;

public class AddBuyerInput
{
    [Required(ErrorMessage = CommonMessageConsts.Annotation_RequiredError)]
    [DefaultValue("ANIL")] // todo: test vaule, remove later
    public string Name { get; set; }
    [Required(ErrorMessage = CommonMessageConsts.Annotation_RequiredError)]
    [DefaultValue("SEZER")] // todo: test vaule, remove later
    public string Surname { get; set; }
    [Required(ErrorMessage = CommonMessageConsts.Annotation_RequiredError)]
    [DefaultValue(typeof(DateTime), "1993-09-10")] // todo: test vaule, remove later
    public DateTime BirthDate { get; set; }
    [Required(ErrorMessage = CommonMessageConsts.Annotation_RequiredError)]
    [DefaultValue(53992119076)] // todo: test vaule, remove later
    public long IdentityNo { get; set; }
}
