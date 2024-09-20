using Athena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.RepositoryInterface
{
    public interface ISubLocationRepository
    {
        public Task<List<SubLocationEntity>> GetSubLocations(int locationSk);
        public Task<List<SubLocationEntity>> GetAllSubLocations();

    }
}
