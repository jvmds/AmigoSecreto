using AmigoSecreto.Entities;

namespace AmigoSecreto.Dtos;

public class GroupOutputDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ISet<UserDto> Users { get; set; } = new HashSet<UserDto>();
}
