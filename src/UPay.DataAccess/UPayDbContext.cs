using Microsoft.EntityFrameworkCore;
using UPay.Domain.Entities;
using UPay.Domain.Enums;
using UPay.Domain.Helpers;

namespace UPay.DataAccess;

public class UPayDbContext : DbContext
{
    public UPayDbContext(DbContextOptions<UPayDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customer { get; set; }
    public virtual DbSet<Transaction> Transaction { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Name)
                .IsRequired()
                .HasComment("Name of the customer");
            entity.Property(e => e.Surname)
                .IsRequired()
                .HasComment("Surname of the customer");
            entity.Property(e => e.IdentityNo)
                .IsRequired()
                .HasComment("Identity number of the customer, mostly Turkish citizen Id number. Aka. TC Kimlik No or TCKN");
            entity.Property(e => e.IdentityNoVerified)
                .IsRequired()
                .HasComment("Is this identity number is verified by the nvi.gov.tr service?");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasComment("Currently have no idea what this is for. Maybe for the status of the customer?"); // todo: update this comment
            // Status might be this: ACTIVE or PASSIVE string(20)
            // https://dev.birlesikodeme.com/api-payzee/tr/api/odeme-servisleri/merchant-list
            
            // Found cardtype: 
            // C: Kredi kartı, D: Debit kart, P: Prepaid kart
            // https://dev.birlesikodeme.com/api-payzee/tr/api/odeme-servisleri/bin-inquiry
        });
        
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.Property(e => e.OrderId)
                .IsRequired()
                .HasComment("Where does this generated from? Is this a foreign key?"); // todo: check this
            entity.Property(e => e.TypeId)
                .IsRequired()
                .HasComment($"Options are: {EnumHelper.GetEnumValuesString<TransactionType>()}");
            entity.Property(e => e.Amount)
                .IsRequired()
                .HasComment("Amount of the transaction. This is TRY, right?");
            entity.Property(e => e.CardPan)
                .IsRequired()
                .HasComment("Is this identity number is verified by the nvi.gov.tr service?");
            entity.Property(e => e.ResponseCode)
                .IsRequired()
                .HasComment("Response code of the transaction. Here's some example codes: https://dev.birlesikodeme.com/api-payzee/tr/test-cards");
            entity.Property(e => e.ResponseMessage)
                .IsRequired()
                .HasComment("Response message of the transaction.");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasComment("Currently have no idea what this is for. Maybe for the status of the customer?"); // todo: update this comment
        });
    }
}