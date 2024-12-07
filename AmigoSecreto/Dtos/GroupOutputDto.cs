using AmigoSecreto.Entities;

namespace AmigoSecreto.Dtos;

public class GroupOutputDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ISet<UserOutputDto> Users { get; set; } = new HashSet<UserOutputDto>();
}
