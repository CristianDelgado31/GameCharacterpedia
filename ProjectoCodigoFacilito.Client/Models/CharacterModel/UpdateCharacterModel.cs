using ProjectoCodigoFacilito.Client.Models.CharacterModel.CharacterInterface;

namespace ProjectoCodigoFacilito.Client.Models.CharacterModel
{
    public class UpdateCharacterModel : IBaseCharacterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Game { get; set; }
        public string Role { get; set; }
        public string History { get; set; }
        public int ModifiedById { get; set; }
        public byte[] ImageStream { get; set;}
        public string nameImageStream { get; set;}
    }
}
