using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Domain.Authorization.Entities;

namespace MS.Persistence.Authorization.Data.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(u => u.Username)
                .HasColumnName("Username")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.PasswordHash)
                .HasColumnName("PasswordHash")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.DateModified)
                .HasColumnName("ModifiedDate");

            builder.Property(u => u.DateCreated)
               .HasColumnName("DateCreated");

            builder.HasMany(u => u.UserRoles)
                .WithOne()
                .HasForeignKey("UserId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasMany(u => u.Tokens)
                .WithOne()
                .HasForeignKey("UserId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
