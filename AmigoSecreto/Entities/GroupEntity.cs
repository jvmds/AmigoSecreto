namespace AmigoSecreto.Entities;

public class GroupEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ISet<UserEntity> Users { get; set; } = new HashSet<UserEntity>();
}
