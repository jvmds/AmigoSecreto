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
        CreateMap<UserEntity, UserOutputDto>()
                        .ForMember(
                                        dest => dest.SecretSanta, 
                                        opt => opt
                                                        .MapFrom(
                                                                        src => src.SecretSanta != null ? new UserOutputDto
                                                                        {
                                                                                        Id = src.SecretSanta.Id, 
                                                                                        FirstName = src.SecretSanta.FirstName, 
                                                                                        LestName = src.SecretSanta.LestName, 
                                                                                        Email = src.SecretSanta.Email,
                                                                                        SecretSanta = null
                                                                        } : null));;
    }
}
