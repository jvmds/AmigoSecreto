using System.ComponentModel.DataAnnotations;
using AmigoSecreto.Entities;

namespace AmigoSecreto.Dtos;

public class GroupInputDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}
