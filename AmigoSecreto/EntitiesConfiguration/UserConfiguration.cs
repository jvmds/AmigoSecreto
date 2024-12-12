using AmigoSecreto.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmigoSecreto.EntitiesConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder
            .HasMany(u => u.UsersGroups)
            .WithOne(j => j.User)
            .HasForeignKey(j => j.UserId)
            .IsRequired(true);
        builder
            .HasMany<UserGroupEntity>()
            .WithOne(j => j.SecretSanta)
            .HasForeignKey(j => j.SecretSantaId)
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
        builder
            .Property(e => e.DateTimeCreation)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
