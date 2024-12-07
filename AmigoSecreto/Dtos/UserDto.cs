using AmigoSecreto.Entities;

namespace AmigoSecreto.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LestName { get; set; } = null!;
    public string Email { get; set; } = null!;
    //public GroupDto? Group { get; set; }
    public UserDto? SecretSanta { get; set; }
}
