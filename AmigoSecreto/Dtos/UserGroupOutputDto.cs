namespace AmigoSecreto.Dtos;

public class UserGroupOutputDto
{
    public int Id { get; set; }
    public UserInputDto User { get; set; } = null!;
    public GroupInputDto Group { get; set; } = null!;
    public UserInputDto? SecreteSanta { get; set; }
}