using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Domain.Models;
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
    public class DishPreparationRepository : IDishPreparationRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<DishPreparationRepository> _logger;

        public DishPreparationRepository(ILogger<DishPreparationRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<DishPreparationEntity>> GetDishPreparations(int dishSk)
        {
            return await _athenaDbcontext.DishPreparationEntity.Where(p => p.DishSk == dishSk).ToListAsync();
        }

        public async Task<bool> SaveDishPreparation(int dishSk, List<DishPreparationsReqModel> dishPreparations)
        {
            foreach (DishPreparationsReqModel dishPreparation in dishPreparations)
            {
                DishPreparationEntity newDishPreparation = new DishPreparationEntity()
                {
                    DishSk = dishSk,
                    DishPrepStepSequence = dishPreparation.DishPrepStepSequence,
                    DishPrepMethod = dishPreparation.DishPrepMethod,
                    DishProcessesSk = dishPreparation.DishProcessesSk,
                    DishProcStepSk = dishPreparation.DishProcStepSk,
                    DishProcSectionSk = dishPreparation.DishProcSectionSk,
                    DishTimeSk = dishPreparation.DishTimeSk,
                    DishPrepTime = dishPreparation.DishPrepTime,
                    DishLowTemp = dishPreparation.DishLowTemp,
                    DishHighTemp = dishPreparation.DishHighTemp,
                    DishHaccpFlag = dishPreparation.DishHaccpFlag
                };

                await _athenaDbcontext.DishPreparationEntity.AddAsync(newDishPreparation);
                await _athenaDbcontext.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> UpdateDishPreparation(int dishSk, List<DishPreparationsReqModel> reqDishPreparations, string userId)
        {
            List<DishPreparationEntity> existingDishPreparations = await _athenaDbcontext.DishPreparationEntity.Where(i => i.DishSk == dishSk).ToListAsync();
            if (existingDishPreparations.Any())
            {
                List<int> requestDishPreparations = reqDishPreparations.Select(i => i.DishPrepSk).ToList();

                List<DishPreparationEntity> dishPrepsToDelete = existingDishPreparations.Where(i => !requestDishPreparations.Contains(i.DishPrepSk)).ToList();

                if (dishPrepsToDelete.Any())
                    _athenaDbcontext.DishPreparationEntity.RemoveRange(dishPrepsToDelete);

                List<DishPreparationEntity> dishPrepsToUpdate = existingDishPreparations.Where(i => requestDishPreparations.Contains(i.DishPrepSk)).ToList();
                if (dishPrepsToUpdate.Any())
                {
                    foreach (DishPreparationEntity dishPreps in existingDishPreparations)
                    {
                        DishPreparationsReqModel? reqDishIngToUpdate = reqDishPreparations.Where(i => i.DishPrepSk == dishPreps.DishPrepSk).SingleOrDefault();
                        if (reqDishIngToUpdate != null)
                        {

                            dishPreps.ModifiedBy = userId;
                            dishPreps.ModifiedDate = DateTime.UtcNow;
                        }
                    }
                }
                await _athenaDbcontext.SaveChangesAsync();

                List<int> existingDishIngIds = existingDishPreparations.Select(i => i.DishPrepSk).ToList();
                if (existingDishIngIds != null && existingDishIngIds.Count > 0)
                    reqDishPreparations = reqDishPreparations.Where(i => !existingDishIngIds.Contains(i.DishPrepSk)).ToList();
            }

            if (reqDishPreparations.Any())
                await SaveDishPreparation(dishSk, reqDishPreparations);

            return true;
        }
    }
}
