using AmigoSecreto.Context;
using AmigoSecreto.Dtos;
using AmigoSecreto.Entities;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AmigoSecreto.Endpoints;

public static class UserEndpoints
{
    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("/users",
                                        async (UserInputDto userInput, AmigoSecretoContext amigoSecretoContext,
                                                        IMapper mapper, 
                                                        IValidator<UserInputDto> validator,
                                                        IConfiguration configuration) =>
                                        {
                                            var validate = await validator.ValidateAsync(userInput);
                                            if (!validate.IsValid)
                                            {
                                                return Results.BadRequest(ErrorDto.CreatedError400(validate.Errors));
                                            }

                                            var userEntity = mapper.Map<UserEntity>(userInput);

                                            await amigoSecretoContext.User.AddAsync(userEntity);
                                            await amigoSecretoContext.SaveChangesAsync();

                                            var userOutputDto = mapper.Map<UserOutputDto>(userEntity);

                                            return Results.Created($"{configuration["BaseUri"]}/users/{userEntity.Id}", userOutputDto);
                                        })
                        .Produces<UserOutputDto>(StatusCodes.Status201Created)
                        .Produces<ErrorDto>(StatusCodes.Status400BadRequest)
                        .Produces<ErrorDto>(StatusCodes.Status500InternalServerError)
                        .WithName("Criar usuário")
                        .WithOpenApi(x => new OpenApiOperation(x)
                        {
                                        Summary = "Cria um novo usuário",
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
                                                return Results.NotFound(ErrorDto.CreatedError404());
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
                        .Produces<ErrorDto>(StatusCodes.Status404NotFound)
                        .Produces<ErrorDto>(StatusCodes.Status500InternalServerError)
                        .WithName("Busca um usuário")
                        .WithOpenApi(x => new OpenApiOperation(x)
                                        {
                                                        Summary = "Busca por um usuário",
                                                        Description = "Busca por um usuário na base de dados usando como chave o ID",
                                                        Tags = [new OpenApiTag { Name = "Usuários" }]
                                        }
                        );

        return app;
    }
}