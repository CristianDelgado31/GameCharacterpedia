namespace ProjectoCodigoFacilito.Client.Models.CharacterModel
{
    public class CreateCharacterModel
    {
        public string Name { get; set; }
        public string Game { get; set; }
        public string History { get; set; }
        public string Role { get; set; }
        public int CreatedById { get; set; }
        public byte[] ImageStream { get; set; }
        public string nameImageStream { get; set; }
    }
}
