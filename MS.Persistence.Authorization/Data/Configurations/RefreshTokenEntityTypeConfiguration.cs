using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Domain.Authorization.Entities;

namespace MS.Persistence.Authorization.Data.Configurations
{
    public class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            // Configurar la clave primaria
            builder.HasKey(rt => rt.Id);

            // Configurar propiedades
            builder.Property(rt => rt.Token)
                .IsRequired()
                .HasMaxLength(256); // Ajusta el tamaño máximo según tus necesidades

            builder.Property(rt => rt.Expires)
                .IsRequired();

            builder.Property(rt => rt.DateCreated)
                .IsRequired();

            builder.Property(rt => rt.Revoked);

            // Configurar la relación con User
            builder.HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Define el comportamiento en cascada

            // Configurar índices
            builder.HasIndex(rt => rt.Token)
                .IsUnique(); // Asegura que los tokens sean únicos
        }
    }
}
