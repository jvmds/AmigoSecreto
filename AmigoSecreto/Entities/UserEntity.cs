namespace AmigoSecreto.Entities;

public class UserEntity : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LestName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public ISet<UserGroupEntity> UsersGroups { get; set; } = new HashSet<UserGroupEntity>();
}
