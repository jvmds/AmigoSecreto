using AmigoSecreto.Entities;
using Microsoft.EntityFrameworkCore;

namespace AmigoSecreto.Context;

public class AmigoSecretoContext(DbContextOptions<AmigoSecretoContext> options) : DbContext(options)
{

    public DbSet<UserEntity> User { get; set; }
    public DbSet<GroupEntity> Groups { get; set; }
    public DbSet<UserGroupEntity> UserGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AmigoSecretoContext).Assembly);
    }

}
