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
    public class LocationRepository : ILocationRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<LocationRepository> _logger;

        public LocationRepository(ILogger<LocationRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<LocationEntity>> GetLocations(string? location)
        {
            var query = _athenaDbcontext.LocationEntity.AsQueryable();

            if (!string.IsNullOrEmpty(location))
                query = query.Where(l => l.LocationCode.Contains(location));

            return await query.Where(l => l.ActiveStatus == true).ToListAsync();
        }
    }
}
