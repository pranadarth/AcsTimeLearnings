using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using Athena.Infrastructure.Database;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Infrastructure.Repository
{
    public class DishLableDetailsRepository : IDishLabelDetailsRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<DishLableDetailsRepository> _logger;

        public DishLableDetailsRepository(ILogger<DishLableDetailsRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<bool> SaveDishLabelDetails(int dishSk, List<int> dishLableTypeIds)
        {
            foreach (int dishLabelTypeId in dishLableTypeIds)
            {
                DishLabelDetailsEntity newDishLabel = new DishLabelDetailsEntity()
                {
                    DishSk = dishSk,
                    LabelTypeId = dishLabelTypeId
                };

                await _athenaDbcontext.DishLabelDetailsEntity.AddAsync(newDishLabel);
                await _athenaDbcontext.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> UpdateDishLabelDetails(int dishSk, List<int> dishLableTypeIds)
        {
            List<DishLabelDetailsEntity> dishLabels = _athenaDbcontext.DishLabelDetailsEntity.Where(d => d.DishSk == dishSk).ToList();
            if (dishLabels != null && dishLabels.Count > 0)
            {
                List<DishLabelDetailsEntity> dishLabelsToDelete = dishLabels.Where(d => !dishLableTypeIds.Contains(d.LabelTypeId)).ToList();
                if (dishLabelsToDelete != null && dishLabelsToDelete.Count > 0)
                {
                    _athenaDbcontext.DishLabelDetailsEntity.RemoveRange(dishLabelsToDelete);

                    List<int> existingLabelIds = dishLabels.Select(d => d.LabelTypeId).ToList();

                    dishLableTypeIds = dishLableTypeIds.Where(l => !existingLabelIds.Contains(l)).ToList();
                }
            }

            await SaveDishLabelDetails(dishSk, dishLableTypeIds);
            return true;
        }
    }
}
