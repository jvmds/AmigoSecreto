namespace AmigoSecreto.Entities;

public class GroupEntity : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ISet<UserGroupEntity> UsersGroups { get; set; } = new HashSet<UserGroupEntity>();
}
