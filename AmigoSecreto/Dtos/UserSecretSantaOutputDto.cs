namespace AmigoSecreto.Dtos;

public class UserSecretSantaOutputDto : UserInputDto
{
    public UserInputDto? SecretSanta { get; set; } = null!;
}