namespace AmigoSecreto.Dtos;

public class UserGroupSecretSantaOutputDto
{
    public GroupInputDto Group { get; set; } = null!;
    public ICollection<UserSecretSantaOutputDto> Users { get; set; } = new HashSet<UserSecretSantaOutputDto>();
}