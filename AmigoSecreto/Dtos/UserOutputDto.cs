namespace AmigoSecreto.Dtos;

public class UserOutputDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LestName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public ISet<GroupOutputDto> Groups { get; set; } = new HashSet<GroupOutputDto>();
    
}
