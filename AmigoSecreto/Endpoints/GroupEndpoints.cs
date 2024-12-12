using AmigoSecreto.Context;
using AmigoSecreto.Dtos;
using AmigoSecreto.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AmigoSecreto.Endpoints;

public static class GroupEndpoints
{
    public static WebApplication MapGroupEndpoints(this WebApplication app)
    {
        app.MapPost("/groups", async (GroupInputDto groupInput, AmigoSecretoContext _amigoSecretoContext, IMapper _mapper) =>
        {
            var groupEntity = _mapper.Map<GroupEntity>(groupInput);

            await _amigoSecretoContext.Groups.AddAsync(groupEntity);
            await _amigoSecretoContext.SaveChangesAsync();

            var groupOutputDto = _mapper.Map<GroupOutputDto>(groupEntity);

            return Results.Created($"{groupEntity.Id}", groupOutputDto);
        })
            .Produces<GroupOutputDto>(StatusCodes.Status201Created)
            .WithName("Criar grupo")
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Cria um novo grupo na base de dados",
                Description = "Cria um novo grupo na base de dados",
                Tags = [new OpenApiTag { Name = "Grupos" }]
            });
        
        app.MapGet("/groups/{groupId:int}", async (int groupId, AmigoSecretoContext _amigoSecretoContext, IMapper _mapper) =>
        {
            var groupEntity = await _amigoSecretoContext
                            .Groups
                            .FirstOrDefaultAsync(g => g.Id == groupId);
            if (groupEntity is null)
            {
                Results.NotFound();
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
        .Produces<GroupOutputDto>(StatusCodes.Status200OK)
        .WithName("Busca por um grupo")
        .WithOpenApi(x => new OpenApiOperation(x)
        {
                        Summary = "Busca por um grupo",
                        Description = "Busca um grupo especifico pelo ID",
                        Tags = [new OpenApiTag { Name = "Grupo" }]
        }
        );
        
        return app;
    }
}
