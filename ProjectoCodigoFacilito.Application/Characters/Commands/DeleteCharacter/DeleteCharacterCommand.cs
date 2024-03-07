using MediatR;

namespace ProjectoCodigoFacilito.Application.Characters.Commands.DeleteCharacter
{
    public class DeleteCharacterCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int ModifiedById { get; set; }
    }
}
