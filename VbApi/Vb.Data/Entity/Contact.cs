using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vb.Base.Entity;

namespace Vb.Data.Entity;

[Table("Contact", Schema = "dbo")]
public class Contact : BaseEntityWithId
{
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    
    public string ContactType { get; set; }
    public string Information { get; set; }
    public bool IsDefault { get; set; }
}
public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.InsertUserId).IsRequired(true);
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.UpdateUserId).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
        
        builder.Property(x => x.CustomerId).IsRequired(true);
        builder.Property(x => x.ContactType).IsRequired(true).HasMaxLength(10);
        builder.Property(x => x.Information).IsRequired(true).HasMaxLength(100);
        builder.Property(x => x.IsDefault).IsRequired(true).HasDefaultValue(false);

        builder.HasIndex(x => x.CustomerId);
        builder.HasIndex(x => new { x.Information, x.ContactType }).IsUnique(true);
    }
}