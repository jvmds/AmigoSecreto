using AmigoSecreto.Dtos;
using AmigoSecreto.Entities;
using AutoMapper;

namespace AmigoSecreto.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        CreateMap<GroupInputDto, GroupEntity>();
        CreateMap<GroupEntity, GroupOutputDto>();
        CreateMap<UserInputDto, UserEntity>();
        CreateMap<UserEntity, UserOutputDto>();
    }
}
