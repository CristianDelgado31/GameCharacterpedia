﻿using ProjectoCodigoFacilito.Client.Models.CharacterModel;

namespace ProjectoCodigoFacilito.Client.Models.UserModel
{
    public class SignInUserModel : BaseUserModel
    {
        public List<GetCharacterModel> ListFavoriteCharacters { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime TokenExpiration { get; set; }

    }
}
