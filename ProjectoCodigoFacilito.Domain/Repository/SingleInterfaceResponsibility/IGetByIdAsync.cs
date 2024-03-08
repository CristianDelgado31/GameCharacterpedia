using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoCodigoFacilito.Domain.Repository.SingleInterfaceResponsibility
{
    public interface IGetByIdAsync<T> where T : class
    {
        public Task<T?> GetByIdAsync(int id);
    }
}
