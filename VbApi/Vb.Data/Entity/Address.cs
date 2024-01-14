using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vb.Base.Entity;

namespace Vb.Data.Entity;

[Table("Address", Schema = "dbo")]
public class Address : BaseEntityWithId
{
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string PostalCode { get; set; }
    public bool IsDefault { get; set; }
}
public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.InsertUserId).IsRequired(true);
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.UpdateUserId).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
        
        builder.Property(x => x.CustomerId).IsRequired(true);
        builder.Property(x => x.Address1).IsRequired(true).HasMaxLength(150);
        builder.Property(x => x.Address2).IsRequired(false).HasMaxLength(150);
        builder.Property(x => x.Country).IsRequired(true).HasMaxLength(100);
        builder.Property(x => x.City).IsRequired(true).HasMaxLength(100);
        builder.Property(x => x.County).IsRequired(false).HasMaxLength(100);
        builder.Property(x => x.PostalCode).IsRequired(false).HasMaxLength(10);
        builder.Property(x => x.IsDefault).IsRequired(true).HasDefaultValue(false);
        
        builder.HasIndex(x => x.CustomerId);
    }
}