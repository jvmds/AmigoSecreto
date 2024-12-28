using AmigoSecreto.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmigoSecreto.EntitiesConfiguration;

public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroupEntity>
{
    public void Configure(EntityTypeBuilder<UserGroupEntity> builder)
    {
        builder
            .Property(e => e.DateTimeCreation)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder
                        .Property(e => e.SecretSantaId)
                        .IsRequired(false);
    }
}
