﻿using AmigoSecreto.Entities;

namespace AmigoSecreto.Dtos;

public class UserOutputDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LestName { get; set; } = null!;
    public string Email { get; set; } = null!;
    
    //public GroupOutputDto Group { get; set; } = null!;
    public UserOutputDto? SecretSanta { get; set; }
    
}
