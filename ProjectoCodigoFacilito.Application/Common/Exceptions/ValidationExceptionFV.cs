using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoCodigoFacilito.Application.Common.Exceptions
{
    public class ValidationExceptionFV : Exception
    {
        public IEnumerable<string> Errors { get; }
        public string RequestType { get; }

        public ValidationExceptionFV(string requestType, IEnumerable<string> errors): base($"Validation failed for request {requestType}")
        {
            RequestType = requestType;
            Errors = errors;
        }
    }
}

