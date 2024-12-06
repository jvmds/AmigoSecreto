using AmigoSecreto.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmigoSecreto.EntitiesConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder
            .HasOne(u => u.SecretSanta)
            .WithOne()
            .HasForeignKey<UserEntity>(u => u.SecretSantaId)
            .IsRequired(false);
        builder
            .Property(u => u.Email)
            .HasAnnotation("ValidationType", "Email");
        builder
            .Property(u => u.FirstName)
            .HasMaxLength(50);
        builder
            .Property(u => u.LestName)
            .HasMaxLength(50);
    }
}
