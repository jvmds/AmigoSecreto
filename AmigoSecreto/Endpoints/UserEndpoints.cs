using AmigoSecreto.Context;
using AmigoSecreto.Dtos;
using AmigoSecreto.Entities;
using AutoMapper;
using Microsoft.OpenApi.Models;

namespace AmigoSecreto.Endpoints;

public static class UserEndpoints
{
    public static WebApplication MapUserEndpoints(this WebApplication app)
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
                Tags = [new OpenApiTag { Name = "Grupos" }]
            });

        return app;
    }
}
