using AmigoSecreto.Context;
using AmigoSecreto.Dtos;
using AmigoSecreto.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AmigoSecreto.Endpoints;

public static class SecretSantaEndpoints
{
    public static WebApplication MapSecretSantaEndpoints(this WebApplication app)
    {
        app.MapPost("/groups", async (GroupInputDto groupInputDto, AmigoSecretoContext _amigoSecretoContext, IMapper _mapper) =>
        {
            var groupEntity = _mapper.Map<GroupEntity>(groupInputDto);

            await _amigoSecretoContext.Groups.AddAsync(groupEntity);
            await _amigoSecretoContext.SaveChangesAsync();

            var groupOutputDto = _mapper.Map<GroupOutputDto>(groupEntity);

            return Results.Created($"{groupEntity.Id}", groupOutputDto);
        })
            .Produces<GroupOutputDto>(StatusCodes.Status201Created)
            .WithName("Criar grupo")
            .WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Summary = "Cria um novo grupo na base de dados",
                Description = "Cria um novo grupo vazio na base de dados",
                Tags = [new OpenApiTag { Name = "Amigo Oculto" }]
            });
        
        app.MapPost("/groups/{groupId:int}/add-users", async (UserInputDto userInputDto, int groupId, AmigoSecretoContext _amigoSecretoContext, IMapper _mapper) =>
                        {
                            var groupEntity = await _amigoSecretoContext.Groups.FindAsync(groupId);
                            if (groupEntity is null)
                            {
                                Results.NotFound();
                            }
                            
                            var userEntity = _mapper.Map<UserEntity>(userInputDto);
                            userEntity.Group = groupEntity;
                            await _amigoSecretoContext.User.AddAsync(userEntity);
                            await _amigoSecretoContext.SaveChangesAsync();
                            
                            var userOutputDto = _mapper.Map<UserOutputDto>(userEntity);
                            
                            return Results.Created($"{userOutputDto.Id}", userOutputDto);
                        })
                        .Produces<UserOutputDto>(StatusCodes.Status201Created)
                        .WithName("Cadastrar usuário")
                        .WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
                        {
                                        Summary = "Cadastrar usuário",
                                        Description = "Cadastra um usuário em um grupo especifico",
                                        Tags = [new OpenApiTag { Name = "Amigo Oculto" }]
                        });
        
        app.MapGet("/groups/{groupId:int}/draw", async (int groupId, bool? selfSelection, AmigoSecretoContext _amigoSecretoContext, IMapper _mapper) =>
                        {
                            var groupEntity = await _amigoSecretoContext
                                            .Groups
                                            .Include(g => g.Users)
                                            .FirstOrDefaultAsync(g => g.Id == groupId);
                            if (groupEntity is null)
                            {
                                Results.NotFound();
                            }
                            
                            var random = new Random();
                            var users = groupEntity!.Users.ToList();
                            var ids = new List<int>();
                            
                            foreach (var currentUser in users)
                            {
                                List<UserEntity> remainingUsers;
                                if (selfSelection ?? false)
                                {
                                    remainingUsers = users
                                                    .Where(u => !ids.Contains(u.Id))
                                                    .ToList();
                                    
                                }
                                else
                                {
                                    remainingUsers = users
                                                    .Where(u => u.Id != currentUser.Id && !ids.Contains(u.Id))
                                                    .ToList();
                                }
                                
                                if (remainingUsers.Count == 0)
                                {
                                    continue;
                                }
                                
                                var positionOfTheDrawnFriend = random.Next(remainingUsers.Count);
                                var secretSanta = remainingUsers[positionOfTheDrawnFriend];
                                currentUser.SecretSanta = secretSanta;
                                ids.Add(secretSanta.Id);
                                _amigoSecretoContext.User.Update(currentUser);
                                await _amigoSecretoContext.SaveChangesAsync();
                            }
                            
                            var groupOutputDto = _mapper.Map<GroupOutputDto>(groupEntity);
                            
                            return Results.Ok(groupOutputDto);
                        })
                        .Produces<GroupOutputDto>(StatusCodes.Status201Created)
                        .WithName("Sortear integrantes")
                        .WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
                        {
                                        Summary = "Sortear integrantes",
                                        Description = "Realiza o sorteio dos integrantes do grupo",
                                        Tags = [new OpenApiTag { Name = "Amigo Oculto" }]
                        });
        
        app.MapGet("/groups/{groupId:int}", async (int groupId, AmigoSecretoContext _amigoSecretoContext, IMapper _mapper) =>
                        {
                            var groupEntity = await _amigoSecretoContext
                                            .Groups
                                            .Include(g => g.Users)
                                            .FirstOrDefaultAsync(g => g.Id == groupId);
                            if (groupEntity is null)
                            {
                                Results.NotFound();
                            }
                            
                            var groupOutputDto = _mapper.Map<GroupOutputDto>(groupEntity);
                            
                            return Results.Ok(groupOutputDto);
                        })
                        .Produces<GroupOutputDto>(StatusCodes.Status201Created)
                        .WithName("Buscar grupo")
                        .WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
                        {
                                        Summary = "Buscar grupo",
                                        Description = "Retorna as informações do grupo e de seus integrantes",
                                        Tags = [new OpenApiTag { Name = "Amigo Oculto" }]
                        });

        return app;
    }
}
