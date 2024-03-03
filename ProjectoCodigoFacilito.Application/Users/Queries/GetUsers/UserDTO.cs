using ProjectoCodigoFacilito.Application.Common.Mapping;
using ProjectoCodigoFacilito.Domain.Entities;

namespace ProjectoCodigoFacilito.Application.Users.Queries.GetUsers;

public record UserDTO(int Id, string Name, string Email, string Password, string Role,
    IEnumerable<Character> ListFavoriteCharacters, bool IsDeleted, DateTime CreatedDate,
    DateTime ModifiedDate);