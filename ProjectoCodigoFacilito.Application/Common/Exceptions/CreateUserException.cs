using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoCodigoFacilito.Application.Common.Exceptions
{
    public class CreateUserException : Exception
    {
        public IEnumerable<string> Errors { get; }

        public CreateUserException(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}

