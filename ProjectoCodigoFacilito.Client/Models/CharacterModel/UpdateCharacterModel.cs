using ProjectoCodigoFacilito.Client.Models.CharacterModel.CharacterInterface;

namespace ProjectoCodigoFacilito.Client.Models.CharacterModel
{
    public class UpdateCharacterModel : IBaseCharacterModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Game { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string History { get; set; } = string.Empty;
        public int ModifiedById { get; set; }
        public byte[] ImageStream { get; set;}
        public string nameImageStream { get; set;}
    }
}
