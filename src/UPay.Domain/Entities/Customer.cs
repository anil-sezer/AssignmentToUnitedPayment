namespace UPay.Domain.Entities;

public class Customer : EntityBase
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; } // Sadece Date'i tutabildiğim, daha az yer kaplayan bir veri yapısı var mı PostgreSQL'de?
    public long IdentityNo { get; set; }
    public bool IdentityNoVerified { get; set; }
    public string Status { get; set; }
}
