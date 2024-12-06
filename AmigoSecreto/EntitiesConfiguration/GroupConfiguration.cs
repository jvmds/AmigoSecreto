using AmigoSecreto.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmigoSecreto.EntitiesConfiguration;

public class GroupConfiguration : IEntityTypeConfiguration<GroupEntity>
{
    public void Configure(EntityTypeBuilder<GroupEntity> builder)
    {
        builder
            .HasMany(g => g.Users)
            .WithOne(u => u.Group)
            .HasForeignKey(u => u.GroupId);
        builder
            .Property(g => g.Name)
            .HasMaxLength(50);
        builder
            .Property(g => g.Description)
            .HasMaxLength(100);
    }
}
