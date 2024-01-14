using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vb.Base.Entity;

namespace Vb.Data.Entity;

[Table("Account", Schema = "dbo")]
public class Account : BaseEntity
{
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public int AccountNumber { get; set; }
    public string IBAN { get; set; }
    public decimal Balance { get; set; }
    public string CurrencyType { get; set; }
    public string Name { get; set; }
    public DateTime OpenDate { get; set; }

    public virtual List<AccountTransaction> AccountTransactions { get; set; }
    public virtual List<EftTransaction> EftTransactions { get; set; }
}

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.Property(x => x.AccountNumber).ValueGeneratedNever();
        
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.InsertUserId).IsRequired(true);
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.UpdateUserId).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.CustomerId).IsRequired(true);
        builder.Property(x => x.AccountNumber).IsRequired(true);
        builder.Property(x => x.IBAN).IsRequired(true).HasMaxLength(34);
        builder.Property(x => x.Balance).IsRequired(true).HasPrecision(18, 4);
        builder.Property(x => x.CurrencyType).IsRequired(true).HasMaxLength(3);
        builder.Property(x => x.Name).IsRequired(false).HasMaxLength(100);
        builder.Property(x => x.OpenDate).IsRequired(true);

        builder.HasIndex(x => x.CustomerId);
        builder.HasIndex(x => x.AccountNumber).IsUnique(true);
        builder.HasKey(x => x.AccountNumber);


        builder.HasMany(x => x.AccountTransactions)
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.AccountId)
            .IsRequired(true);

        builder.HasMany(x => x.EftTransactions)
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.AccountId)
            .IsRequired(true);
    }
}