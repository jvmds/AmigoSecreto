namespace AmigoSecreto.Dtos;

public class UserOutputDto
{
    public int Id { get; init; }
    public string FirstName { get; init; } = null!;
    public string LestName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public ISet<GroupOutputDto> Groups { get; set; } = new HashSet<GroupOutputDto>();
    
}
