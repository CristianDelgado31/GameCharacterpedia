namespace ProjectoCodigoFacilito.Client.Models.CharacterModel.CharacterInterface
{
    public interface IBaseCharacterModel
    {
        public string Name { get; set; }
        public string Game { get; set; }
        public string History { get; set; }
        public string Role { get; set; }
        public byte[] ImageStream { get; set; }
        public string nameImageStream { get; set; }
    }
}
