using AutoMapper;
using ProjectoCodigoFacilito.Application.Characters.Commands.CreateCharacter;
using ProjectoCodigoFacilito.Application.Characters.Commands.UpdateCharacter;
using ProjectoCodigoFacilito.Application.Characters.Queries.GetCharacters;
using ProjectoCodigoFacilito.Application.Users.Commands.CreateUser;
using ProjectoCodigoFacilito.Application.Users.Commands.UpdateUser;
using ProjectoCodigoFacilito.Application.Users.Queries.GetUsers;
using ProjectoCodigoFacilito.Domain.Entities;


namespace ProjectoCodigoFacilito.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserCommand, User>(); // Esto retorna un User
            CreateMap<UpdateUserCommand, User>();
            CreateMap<User,UserDTO>();
            CreateMap<CreateCharacterCommand, Character>();
            CreateMap<Character, CharacterDTO>();
            CreateMap<UpdateCharacterCommand, Character>();
        }
    }
}
