using AmigoSecreto.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;

namespace AmigoSecreto.EntitiesConfiguration;

public class GroupConfiguration : IEntityTypeConfiguration<GroupEntity>
{
    public void Configure(EntityTypeBuilder<GroupEntity> builder)
    {
        builder
            .HasMany(u => u.UsersGroups)
            .WithOne(j => j.Group)
            .HasForeignKey(j => j.GroupId)
            .IsRequired(true);
        builder
            .Property(g => g.Name)
            .HasMaxLength(50);
        builder
            .Property(g => g.Description)
            .HasMaxLength(100);
        builder
            .Property(e => e.DateTimeCreation)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
