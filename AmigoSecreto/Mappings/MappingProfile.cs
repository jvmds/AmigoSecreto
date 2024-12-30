using AmigoSecreto.Dtos;
using AmigoSecreto.Entities;
using AutoMapper;

namespace AmigoSecreto.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        CreateMap<GroupInputDto, GroupEntity>().ReverseMap();
        CreateMap<GroupEntity, GroupOutputDto>().ReverseMap();
        CreateMap<UserInputDto, UserEntity>().ReverseMap();
        CreateMap<UserEntity, UserOutputDto>().ReverseMap();
        CreateMap<UserGroupEntity, UserGroupOutputDto>().ReverseMap();
    }
}
