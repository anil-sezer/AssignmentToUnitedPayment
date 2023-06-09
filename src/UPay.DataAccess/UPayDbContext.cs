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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Name)
                .HasComment("Name of the customer");
            entity.Property(e => e.Surname)
                .HasComment("Surname of the customer");
            entity.Property(e => e.IdentityNo)
                .HasComment("Identity number of the customer, mostly Turkish citizen Id number. Aka. TC Kimlik No or TCKN");
            entity.Property(e => e.IdentityNoVerified)
                .HasComment("Is this identity number is verified by the nvi.gov.tr service?");
            entity.Property(e => e.Status)
                .HasComment("Currently have no idea what this is for. Maybe for the status of the customer?"); // todo: update this comment
        });
        
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.Property(e => e.OrderId)
                .HasComment("Where does this generated from? Is this a foreign key?"); // todo: check this
            entity.Property(e => e.TypeId)
                .HasComment($"Options are: {EnumHelper.GetEnumValuesString<TransactionType>()}");
            entity.Property(e => e.Amount)
                .HasComment("Amount of the transaction. This is TRY, right?");
            entity.Property(e => e.CardPan)
                .HasComment("Is this identity number is verified by the nvi.gov.tr service?");
            entity.Property(e => e.ResponseCode)
                .HasComment("Response code of the transaction. Here's some example codes: https://dev.birlesikodeme.com/api-payzee/tr/test-cards");
            entity.Property(e => e.ResponseMessage)
                .HasComment("Response message of the transaction.");
            entity.Property(e => e.Status)
                .HasComment("Currently have no idea what this is for. Maybe for the status of the customer?"); // todo: update this comment
        });
    }
}