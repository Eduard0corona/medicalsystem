using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Domain.Authorization.Entities;

namespace MS.Persistence.Authorization.Data.Configurations
{
    public class TokenEntityTypeConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.ToTable("Tokens");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(t => t.Value)
                .HasColumnName("Value")
                .IsRequired()
                .HasMaxLength(maxLength: int.MaxValue);

            builder.Property(t => t.ExpiryDate)
                .HasColumnName("ExpiryDate")
                .IsRequired();
        }
    }
}
