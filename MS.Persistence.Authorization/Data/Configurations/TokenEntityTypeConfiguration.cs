using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Domain.Authorization.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .HasMaxLength(255);

            builder.Property(t => t.ExpiryDate)
                .HasColumnName("ExpiryDate")
                .IsRequired();
        }
    }
}
