namespace AmigoSecreto.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LestName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public int? GroupId { get; set; }
    public int? SecretSantaId { get; set; }

    public GroupEntity? Group { get; set; }
    public UserEntity? SecretSanta { get; set; }
}
