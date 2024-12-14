namespace AmigoSecreto.Entities;

public class UserGroupEntity : BaseEntity
{
    public int UserId { get; set; }
    public int? SecretSantaId { get; set; }
    public int GroupId { get; set; }

    public UserEntity User { get; set; } = null!;
    public UserEntity? SecretSanta { get; set; }
    public GroupEntity Group { get; set; } = null!;
}
