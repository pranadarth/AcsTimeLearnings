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
    public class LocationMenuTypeMappingRepository : ILocationMenuTypeMappingRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<LocationMenuTypeMappingRepository> _logger;

        public LocationMenuTypeMappingRepository(ILogger<LocationMenuTypeMappingRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<LocationMenuTypeMappingEntity> GetMenuTypeLocationMap(int dishMenuTypeId, int locationSk, int? subLocationSk)
        {
            var query = _athenaDbcontext.LocationMenuTypeMappingEntity
                                               .Where(l => l.LocationSk == locationSk && l.DishMenuTypeId == dishMenuTypeId).AsQueryable();

            if (subLocationSk != null && subLocationSk > 0)
                query = query.Where(x => x.SubLocationSk == subLocationSk);

            return await query.SingleOrDefaultAsync();
        }
    }
}
