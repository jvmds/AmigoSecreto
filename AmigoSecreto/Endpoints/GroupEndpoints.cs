using AmigoSecreto.Context;
using AmigoSecreto.Dtos;
using AmigoSecreto.Entities;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AmigoSecreto.Endpoints;

public static class GroupEndpoints
{
    public static WebApplication MapGroupEndpoints(this WebApplication app)
    {
        app.MapPost("/groups",
                                        async (GroupInputDto groupInput, AmigoSecretoContext amigoSecretoContext,
                                                        IMapper mapper, IValidator<GroupInputDto> validator) =>
                                        {
                                            if (!validator.Validate(groupInput).IsValid)
                                            {
                                                return Results.BadRequest(ErrorDto.CreatedError400(validator.Validate(groupInput).Errors));
                                            }
                                                
                                            var groupEntity = mapper.Map<GroupEntity>(groupInput);

                                            await amigoSecretoContext.Groups.AddAsync(groupEntity);
                                            await amigoSecretoContext.SaveChangesAsync();

                                            var groupOutputDto = mapper.Map<GroupOutputDto>(groupEntity);

                                            return Results.Created($"{groupEntity.Id}", groupOutputDto);

                                        })
                        .Produces<GroupOutputDto>(StatusCodes.Status201Created)
                        .Produces<ErrorDto>(StatusCodes.Status400BadRequest)
                        .Produces<ErrorDto>(StatusCodes.Status500InternalServerError)
                        .WithName("Criar grupo")
                        .WithOpenApi(x => new OpenApiOperation(x)
                        {
                                        Summary = "Cria um novo grupo na base de dados",
                                        Description = "Cria um novo grupo na base de dados",
                                        Tags = [new OpenApiTag { Name = "Grupos" }]
                        });

        app.MapGet("/groups/{groupId:int}",
                                        async (int groupId, AmigoSecretoContext _amigoSecretoContext,
                                                        IMapper _mapper) =>
                                        {
                                            var groupEntity = await _amigoSecretoContext
                                                            .Groups
                                                            .FirstOrDefaultAsync(g => g.Id == groupId);
                                            if (groupEntity is null)
                                            {
                                                return Results.NotFound(ErrorDto.CreatedError404());
                                            }

                                            var usersEntitiesIds = await _amigoSecretoContext
                                                            .UserGroups
                                                            .Where(u => u.GroupId == groupEntity!.Id)
                                                            .Select(u => u.UserId)
                                                            .ToHashSetAsync();

                                            var usersEntities = new HashSet<UserEntity>();

                                            if (usersEntitiesIds.Count > 0)
                                            {
                                                usersEntities = await _amigoSecretoContext
                                                                .User
                                                                .Where(g => usersEntitiesIds.Contains(g.Id))
                                                                .ToHashSetAsync();
                                            }

                                            var groupOutputDto = _mapper.Map<GroupOutputDto>(groupEntity);
                                            groupOutputDto.Users = _mapper.Map<HashSet<UserOutputDto>>(usersEntities);

                                            return Results.Ok(groupOutputDto);
                                        })
                        .Produces<GroupOutputDto>()
                        .Produces<ErrorDto>(StatusCodes.Status404NotFound)
                        .Produces<ErrorDto>(StatusCodes.Status500InternalServerError)
                        .WithName("Buscar por um grupo")
                        .WithOpenApi(x => new OpenApiOperation(x)
                        {
                                        Summary = "Buscar por um grupo",
                                        Description = "Buscar um grupo especifico pelo ID",
                                        Tags = [new OpenApiTag { Name = "Grupos" }]
                        });


        return app;
    }
}