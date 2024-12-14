using AmigoSecreto.Context;
using AmigoSecreto.Dtos;
using AmigoSecreto.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AmigoSecreto.Endpoints;

public static class UserEndpoints
{
    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("/users",
                                        async (UserInputDto userInput, AmigoSecretoContext _amigoSecretoContext,
                                                        IMapper _mapper) =>
                                        {
                                            var userEntity = _mapper.Map<UserEntity>(userInput);

                                            await _amigoSecretoContext.User.AddAsync(userEntity);
                                            await _amigoSecretoContext.SaveChangesAsync();

                                            var userOutputDto = _mapper.Map<UserOutputDto>(userEntity);

                                            return Results.Created($"{userEntity.Id}", userOutputDto);
                                        })
                        .Produces<UserOutputDto>(StatusCodes.Status201Created)
                        .WithName("Criar usuário")
                        .WithOpenApi(x => new OpenApiOperation(x)
                        {
                                        Summary = "Cria um novo usuário na base de dados",
                                        Description = "Cria um novo usuário na base de dados",
                                        Tags = [new OpenApiTag { Name = "Usuários" }]
                        });

        app.MapGet("/users/{userId:int}",
                                        async (int userId, AmigoSecretoContext _amigoSecretoContext, IMapper _mapper) =>
                                        {
                                            var userEntity = await _amigoSecretoContext
                                                            .User
                                                            .FirstOrDefaultAsync(g => g.Id == userId);
                                            if (userEntity is null)
                                            {
                                                Results.NotFound();
                                            }

                                            var groupsEntitiesIds = await _amigoSecretoContext
                                                            .UserGroups
                                                            .Where(u => u.UserId == userEntity!.Id)
                                                            .Select(u => u.GroupId)
                                                            .ToHashSetAsync();

                                            var groupsEntities = new HashSet<GroupEntity>();

                                            if (groupsEntitiesIds.Count > 0)
                                            {
                                                groupsEntities = await _amigoSecretoContext
                                                                .Groups
                                                                .Where(g => groupsEntitiesIds.Contains(g.Id))
                                                                .ToHashSetAsync();
                                            }

                                            var userOutputDto = _mapper.Map<UserOutputDto>(userEntity);
                                            userOutputDto.Groups = _mapper.Map<HashSet<GroupOutputDto>>(groupsEntities);

                                            return Results.Ok(userOutputDto);
                                        })
                        .Produces<UserOutputDto>(StatusCodes.Status200OK)
                        .WithName("Busca por um usuário")
                        .WithOpenApi(x => new OpenApiOperation(x)
                                        {
                                                        Summary = "Busca por um usuário",
                                                        Description = "Busca um usuário especifico pelo ID",
                                                        Tags = [new OpenApiTag { Name = "Usuário" }]
                                        }
                        );

        return app;
    }
}