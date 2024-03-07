using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoCodigoFacilito.Application.Characters.Commands.UpdateCharacter
{
    public class UpdateCharacterCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Game { get; set; }
        public string Role { get; set; }
        public string History { get; set; }
        public int ModifiedById { get; set; }
        public byte[] ImageStream { get; set; }
        public string nameImageStream { get; set; }
    }
}
