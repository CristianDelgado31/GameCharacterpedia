using ProjectoCodigoFacilito.Client.Models.CharacterModel.CharacterInterface;

namespace ProjectoCodigoFacilito.Client.Models.CharacterModel
{
    public class CreateCharacterModel : IBaseCharacterModel
    {
        public string Name { get; set; } = string.Empty;
        public string Game { get; set; } = string.Empty;
        public string History { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int CreatedById { get; set; } // Atributo propio de CreateCharacterModel
        public byte[] ImageStream { get; set; }
        public string nameImageStream { get; set; }
    }
}
