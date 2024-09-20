using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Infrastructure.Repository
{
    public class SubLocationRepository : ISubLocationRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<SubLocationRepository> _logger;

        public SubLocationRepository(ILogger<SubLocationRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }


        public async Task<List<SubLocationEntity>> GetSubLocations(int locationSk)
        {
            return await _athenaDbcontext.SubLocationEntity.Where(s => s.LocationSk == locationSk && s.ActiveStatus == true).ToListAsync();
        }

        public async Task<List<SubLocationEntity>> GetAllSubLocations()
        {
            return await _athenaDbcontext.SubLocationEntity.Where(s => s.ActiveStatus == true).ToListAsync();
        }
    }
}
