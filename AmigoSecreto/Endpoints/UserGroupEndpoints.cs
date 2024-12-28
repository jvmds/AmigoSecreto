using AmigoSecreto.Context;
using AmigoSecreto.Dtos;
using AmigoSecreto.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AmigoSecreto.Endpoints;

public static class UserGroupEndpoints
{
    public static WebApplication MapUserGroupEndpoints(this WebApplication app)
    {
        app.MapPost("/user-group", async (
                                        UserGroupInputDto userGroupDto,
                                        AmigoSecretoContext _amigoSecretoContext,
                                        IMapper _mapper) =>
                        {
                            var groupEntity = await _amigoSecretoContext
                                            .Groups
                                            .FindAsync(userGroupDto.GroupId);
                            if (groupEntity is null)
                            {
                                return Results.NotFound();
                            }

                            var userEntity = await _amigoSecretoContext
                                            .User
                                            .FindAsync(userGroupDto.UserId);
                            if (userEntity is null)
                            {
                                return Results.NotFound();
                            }

                            var userGroupEntity = await _amigoSecretoContext
                                                                  .UserGroups
                                                                  .FirstOrDefaultAsync(u =>
                                                                                  u.GroupId == userGroupDto.GroupId
                                                                                  && u.UserId == userGroupDto.UserId);
                            if (userGroupEntity is null)
                            {
                                userGroupEntity = new UserGroupEntity { User = userEntity, Group = groupEntity };
                                await _amigoSecretoContext.UserGroups.AddAsync(userGroupEntity);
                                await _amigoSecretoContext.SaveChangesAsync();
                            }

                            var userGroupOutputDto = new UserGroupOutputDto()
                            {
                                            User = _mapper.Map<UserInputDto>(userEntity),
                                            Group = _mapper.Map<GroupInputDto>(groupEntity), Id = userGroupEntity.Id,
                                            SecretSanta = null
                            };

                            return Results.Created($"{userGroupOutputDto.Id}", userGroupOutputDto);
                        })
                        .Produces<UserGroupOutputDto>(StatusCodes.Status201Created)
                        .WithName("Vincular usuário a um grupo")
                        .WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
                        {
                                        Summary = "Vincular usuário a um grupo",
                                        Description = "Vincula um usuário a um grupo",
                                        Tags = [new OpenApiTag { Name = "Usuário e Grupo" }]
                        });

        app.MapPatch("/user-group/{userId:int}/{groupId:int}", async (
                                        int userId,
                                        int groupId,
                                        UserGroupSecreteSantaInputDto secreteSantaInputDto,
                                        AmigoSecretoContext _amigoSecretoContext,
                                        IMapper _mapper) =>
                        {
                            var userGroupEntity = await _amigoSecretoContext
                                            .UserGroups
                                            .FirstOrDefaultAsync(u =>
                                                            u.UserId == userId
                                                            && u.GroupId == groupId);
                            if (userGroupEntity is null)
                            {
                                return Results.NotFound();
                            }

                            if (secreteSantaInputDto.SecreteSantaId is null)
                            {
                                userGroupEntity.SecretSanta = null;
                                userGroupEntity.SecretSantaId = null;

                                _amigoSecretoContext.UserGroups.Update(userGroupEntity);
                            }
                            else
                            {
                                var userGroupSecreteSantaEntity = await _amigoSecretoContext
                                                .UserGroups
                                                .Include(u => u.User)
                                                .FirstOrDefaultAsync(u =>
                                                                u.GroupId == groupId
                                                                && u.UserId == secreteSantaInputDto.SecreteSantaId);
                                if (userGroupSecreteSantaEntity is null)
                                {
                                    return Results.NotFound();
                                }

                                userGroupEntity.SecretSanta = userGroupSecreteSantaEntity.User;

                                _amigoSecretoContext.UserGroups.Update(userGroupEntity);


                                var userGroupOtherRelationships = await _amigoSecretoContext
                                                .UserGroups
                                                .FirstOrDefaultAsync(u =>
                                                                u.GroupId == groupId
                                                                && u.SecretSantaId ==
                                                                secreteSantaInputDto.SecreteSantaId);

                                if (userGroupOtherRelationships is not null)
                                {
                                    userGroupOtherRelationships.SecretSanta = null;
                                    userGroupOtherRelationships.SecretSantaId = null;
                                    _amigoSecretoContext.UserGroups.Update(userGroupOtherRelationships);
                                }
                            }

                            await _amigoSecretoContext.SaveChangesAsync();

                            var userGroupOutputDto = _mapper.Map<UserGroupOutputDto>(userGroupEntity);

                            return Results.Ok(userGroupOutputDto);
                        })
                        .Produces<UserGroupOutputDto>()
                        .WithName("Vincular amigo secreto")
                        .WithOpenApi(x => new OpenApiOperation(x)
                        {
                                        Summary = "Vincular amigo secreto",
                                        Description = "Vincula o amigo secreto de um usuário",
                                        Tags = [new OpenApiTag { Name = "Amigo Oculto" }]
                        });

        app.MapGet("/user-group/{userId:int}/{groupId:int}",
                                        async (int userId, int groupId, AmigoSecretoContext _amigoSecretoContext,
                                                        IMapper _mapper) =>
                                        {
                                            var userGroupEntity = await _amigoSecretoContext
                                                            .UserGroups
                                                            .Include(u => u.User)
                                                            .Include(u => u.Group)
                                                            .Include(u => u.SecretSanta)
                                                            .FirstOrDefaultAsync(u =>
                                                                            u.UserId == userId && u.GroupId == groupId);
                                            if (userGroupEntity is null)
                                            {
                                                Results.NotFound();
                                            }

                                            var userGroupOutputDto = _mapper.Map<UserGroupOutputDto>(userGroupEntity);

                                            return Results.Ok(userGroupOutputDto);
                                        })
                        .Produces<UserGroupOutputDto>()
                        .WithName("Obter amigo secreto")
                        .WithOpenApi(x => new OpenApiOperation(x)
                        {
                                        Summary = "Obteramigo secreto",
                                        Description = "Obter o amigo secreto de um usuário",
                                        Tags = [new OpenApiTag { Name = "Amigo Oculto" }]
                        });

        app.MapPost("/user-group/draw",
                                        async (UserGroupDrawInputDto userGroupDrawInputDto,
                                                        AmigoSecretoContext _amigoSecretoContext,
                                                        IMapper _mapper) =>
                                        {
                                            var groupEntity =
                                                            await _amigoSecretoContext.Groups.FindAsync(
                                                                            userGroupDrawInputDto.GroupId);
                                            if (groupEntity is null)
                                            {
                                                return Results.NotFound();
                                            }

                                            var userGroupEntityList = await _amigoSecretoContext
                                                            .UserGroups
                                                            .Include(u => u.User)
                                                            .Where(u =>
                                                                            u.GroupId == userGroupDrawInputDto.GroupId)
                                                            .ToListAsync();

                                            var random = new Random();
                                            var usersEntity = userGroupEntityList.Select(u => u.User).ToList();
                                            var alreadySelectedUsersIds = new List<int>();
                                            var userSecretSanta = new HashSet<UserSecretSantaOutputDto>();

                                            foreach (var userGroupEntity in userGroupEntityList)
                                            {
                                                var usersIds = userGroupEntityList
                                                                .Where(u => u.UserId != userGroupEntity.UserId 
                                                                            && !alreadySelectedUsersIds.Contains(u.UserId))
                                                                .Select(u => u.UserId)
                                                                .ToList();
                                                //usersIds.Remove(userGroupEntity.UserId);
                                                //usersIds.ExceptWith(alreadySelectedUsersIds);
                                                var id = usersIds[random.Next(usersIds.Count)];
                                                userGroupEntity.SecretSanta = usersEntity.First(u => u.Id == id);

                                                _amigoSecretoContext.UserGroups.Update(userGroupEntity);
                                                alreadySelectedUsersIds.Add(id);
                                                
                                                userSecretSanta.Add(new UserSecretSantaOutputDto()
                                                {
                                                                FirstName = userGroupEntity.User.FirstName,
                                                                LestName = userGroupEntity.User.LestName,
                                                                Email = userGroupEntity.User.Email,
                                                                SecretSanta = new UserInputDto()
                                                                {
                                                                                FirstName = userGroupEntity.SecretSanta
                                                                                                .FirstName,
                                                                                LestName = userGroupEntity.SecretSanta
                                                                                                .LestName,
                                                                                Email = userGroupEntity.SecretSanta
                                                                                                .Email
                                                                }
                                                });
                                            }

                                            await _amigoSecretoContext.SaveChangesAsync();
                                            
                                            return Results.Ok(new UserGroupSecretSantaOutputDto()
                                            {
                                                            Group = new GroupInputDto()
                                                            {
                                                                            Name = groupEntity.Name,
                                                                            Description = groupEntity.Description
                                                            },
                                                            Users = userSecretSanta
                                            });
                                        })
                        .Produces<UserGroupSecretSantaOutputDto>()
                        .WithName("Sorteia os amigos secretos")
                        .WithOpenApi(x => new OpenApiOperation(x)
                        {
                                        Summary = "Sorteia os amigos secretos",
                                        Description = "Sorteia os amigos secretos",
                                        Tags = [new OpenApiTag { Name = "Amigo Oculto" }]
                        });

        return app;
    }
}