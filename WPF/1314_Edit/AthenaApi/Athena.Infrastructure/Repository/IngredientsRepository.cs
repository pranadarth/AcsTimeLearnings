using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using Athena.Domain.Models.Reports;
using Athena.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Infrastructure.Repository
{
    public class IngredientsRepository : IIngredientsRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<IngredientsRepository> _logger;

        public IngredientsRepository(ILogger<IngredientsRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<IngredientsMaster> GetByIngNameProductCodeAndSupplier(string ingName, string productCode, string supplier)
        {
            return await _athenaDbcontext.IngredientsMaster.Where(x => x.IngredientsName == ingName && x.SupplierReferenceNo == productCode && x.SupplierName == supplier).SingleOrDefaultAsync();
        }

        public async Task<IngredientsMaster> GetByProductCodeAndSupplier(string productCode, string supplier)
        {
            return await _athenaDbcontext.IngredientsMaster.Where(x => x.SupplierReferenceNo == productCode && x.SupplierName == supplier).FirstOrDefaultAsync();
        }

        public async Task<List<IngredientContractStatus>> GetContractedStatus()
        {
            return await _athenaDbcontext.IngredientContractStatus.Where(s => s.ActiveStatus == true).ToListAsync();
        }

        public async Task<List<Country>> GetCountries()
        {
            return await _athenaDbcontext.Countries.Where(c => c.ActiveStatus == true).ToListAsync();
        }

        public async Task<List<FoodGroup>> GetFoodgroups()
        {
            return await _athenaDbcontext.FoodGroup.Where(fg => fg.ActiveStatus == true).ToListAsync();
        }

        public async Task<IngredientsDbModel> GetIngredients(string supplierName, int recsPerPage, int currPageNo)
        {
            int skip = recsPerPage * currPageNo;
            if (!string.IsNullOrWhiteSpace(supplierName))
            {
                return new IngredientsDbModel
                {
                    Ingredients = await _athenaDbcontext.IngredientsMaster.Where(i => i.SupplierName == supplierName)
                                                         .Skip(skip)
                                                         .Take(recsPerPage).ToListAsync(),
                    TotalIngredients = await _athenaDbcontext.IngredientsMaster.Where(i => i.SupplierName == supplierName).CountAsync()
                };
            }
            else
            {
                return new IngredientsDbModel
                {
                    Ingredients = await _athenaDbcontext.IngredientsMaster.Skip(skip)
                                                         .Take(recsPerPage).ToListAsync(),
                    TotalIngredients = await _athenaDbcontext.IngredientsMaster.CountAsync()
                };
            }
        }

        public async Task<List<MeasureOptions>> GetPackUOM()
        {
            return await _athenaDbcontext.MeasureOptions.Where(m => m.ActiveStatus == true).ToListAsync();
        }

        public async Task<List<IngredientStatus>> GetStatus()
        {
            return await _athenaDbcontext.IngredientStatus.Where(s => s.ActiveStatus == true).ToListAsync();
        }

        public async Task<List<Supplier>> GetSuppliers()
        {
            return await _athenaDbcontext.Supplier.Where(s => s.ActiveStatus == true).ToListAsync();
        }
        public async Task<List<IngredientTypeEntity>> GetIngredientTypes()
        {
            return await _athenaDbcontext.IngredientTypeEntity.Where(s => s.ActiveStatus == true).ToListAsync();
        }
        public async Task<object> Save(SaveIngredientReqModel reqData)
        {
            IngredientsMaster newIngredient = new IngredientsMaster()
            {
                IngredientsName = reqData.IngredientsName,
                CalculationMethod = reqData.PackUnitOfMeasure,
                SupplierName = reqData.SupplierName,
                MinimumOrderQuantity = reqData.MinimumOrderQuantity,
                SpecificationDescription = reqData.SpecificationDescription,
                SupplierId = reqData.SupplierId,
                SupplierReferenceNo = reqData.SupplierReferenceNo,
                StoreSk = reqData.StoreSk,
                SalePrice = reqData.SalePrice,
                IsOnMenu = reqData.IsOnMenu,
                MeasureOptionId = reqData.MeasureOptionId,
                Weight = reqData.Weight,
                McwCode = reqData.McwCode,
                CountrySk = reqData.CountrySk,
                PrepYield = reqData.PrepYield,
                FoodGroupSk = reqData.FoodGroupSk,
                GenericYield = reqData.GenericYield,
                Barcode = reqData.Barcode,
                IngredientTypeId = reqData.IngredientTypeId,

                StatusSk = reqData.StatusSk,
                ContractStatusSk = reqData.ContractStatusSk,
                CreatedBy = reqData.UserId,
                CreatedDate = DateTime.UtcNow
            };

            await _athenaDbcontext.IngredientsMaster.AddAsync(newIngredient);
            await _athenaDbcontext.SaveChangesAsync();

            IngredientsCosting ingredientsMasterCosting = new IngredientsCosting()
            {
                IngSk = newIngredient.IngSk,
                UnitPrice = reqData.Cost,
                PackSize = reqData.PackSize,
                PackCost = reqData.PackCost
            };
            await _athenaDbcontext.IngredientsCosting.AddAsync(ingredientsMasterCosting);
            await _athenaDbcontext.SaveChangesAsync();

            return newIngredient.IngSk;
        }

        public async Task<IngredientsDbModel> SearchIngredients(string? supplierName, string searchText, float? cost, string supplierReferenceNo, IngredientSearchRequestModel reqData, int recsPerPage, int currPageNo)
        {
            var initialQuery = from im in _athenaDbcontext.IngredientsMaster
                               select new
                               {
                                   IngredientsMaster = im,
                                   IngredientsMasterAllergensEntity = (IngredientsMasterAllergensEntity)null,
                                   IngredientsCosting = (IngredientsCosting)null,
                               };

            if (reqData != null && reqData.AllergenIds != null && reqData.AllergenIds.Count > 0)
            {
                initialQuery = from dm in initialQuery
                               join ima in _athenaDbcontext.IngredientsMasterAllergensEntity
                               on dm.IngredientsMaster.IngSk equals ima.IngSk
                               select new
                               {
                                   IngredientsMaster = dm.IngredientsMaster,
                                   IngredientsMasterAllergensEntity = ima,
                                   IngredientsCosting = dm.IngredientsCosting,
                               };
            }

            if (cost != null)
            {
                initialQuery = from dm in initialQuery
                               join ic in _athenaDbcontext.IngredientsCosting
                               on dm.IngredientsMaster.IngSk equals ic.IngSk
                               select new
                               {
                                   IngredientsMaster = dm.IngredientsMaster,
                                   IngredientsMasterAllergensEntity = dm.IngredientsMasterAllergensEntity,
                                   IngredientsCosting = ic,
                               };
            }


            var query = initialQuery.AsQueryable();

            if (!string.IsNullOrEmpty(supplierName))
                query = query.Where(x => x.IngredientsMaster.SupplierName == supplierName);


            query = query.Where(i => i.IngredientsMaster.IngredientsName.Contains(searchText)
                             || i.IngredientsMaster.SupplierName.Contains(searchText)
                             || i.IngredientsMaster.CalculationMethod.Contains(searchText)
                             || i.IngredientsMaster.SupplierReferenceNo.Contains(searchText));

            if (!string.IsNullOrWhiteSpace(supplierReferenceNo))
                query = query.Where(x => x.IngredientsMaster.SupplierReferenceNo == supplierReferenceNo);

            if (reqData != null && reqData.MeasureOptionIds != null && reqData.MeasureOptionIds.Count > 0)
                query = query.Where(x => reqData.MeasureOptionIds.Contains(x.IngredientsMaster.MeasureOptionId));

            if (reqData != null && reqData.SupplierIds != null && reqData.SupplierIds.Count > 0)
                query = query.Where(x => reqData.SupplierIds.Contains(x.IngredientsMaster.SupplierId));

            if (cost != null)
                query = query.Where(x => x.IngredientsCosting.UnitPrice == cost);

            if (reqData != null && reqData.AllergenIds != null && reqData.AllergenIds.Count > 0)
                query = query.Where(x => !reqData.AllergenIds.Contains(x.IngredientsMasterAllergensEntity.AllergenId));

            int skip = recsPerPage * currPageNo;

            //if (string.IsNullOrWhiteSpace(supplierName))
            //{

            //    return await _athenaDbcontext.IngredientsMaster.Where(i => (i.IngredientsName.Contains(searchText)
            //                                                         || i.SupplierName.Contains(searchText)
            //                                                          || i.CalculationMethod.Contains(searchText)
            //                                                           || i.SupplierReferenceNo.Contains(searchText)))
            //                                             .Skip(skip)
            //                                             .Take(recsPerPage).ToListAsync();
            //}
            //else
            //{
            //    return await _athenaDbcontext.IngredientsMaster.Where(i => i.SupplierName == supplierName && (i.IngredientsName.Contains(searchText)
            //                                                        || i.SupplierName.Contains(searchText)
            //                                                         || i.CalculationMethod.Contains(searchText)
            //                                                          || i.SupplierReferenceNo.Contains(searchText)))
            //                                            .Skip(skip)
            //                                            .Take(recsPerPage).ToListAsync();
            //}

            List<IngredientsMaster> ingredients = await query.Select(i => i.IngredientsMaster).Skip(skip).Take(recsPerPage).ToListAsync();

            return new IngredientsDbModel
            {
                Ingredients = ingredients,
                TotalIngredients = await query.Select(i => i.IngredientsMaster).CountAsync()
            };
        }

        public async Task<IngredientsDbModel> GetIngredientsName(string supplierName, string ingredientName, float? cost, string supplierReferenceNo, IngredientSearchRequestModel reqData, int recsPerPage, int currPageNo)
        {
            var initialQuery = from im in _athenaDbcontext.IngredientsMaster
                               select new
                               {
                                   IngredientsMaster = im,
                                   IngredientsMasterAllergensEntity = (IngredientsMasterAllergensEntity)null,
                                   IngredientsCosting = (IngredientsCosting)null,
                               };

            if (reqData != null && reqData.AllergenIds != null && reqData.AllergenIds.Count > 0)
            {
                initialQuery = from dm in initialQuery
                               join ima in _athenaDbcontext.IngredientsMasterAllergensEntity
                               on dm.IngredientsMaster.IngSk equals ima.IngSk
                               select new
                               {
                                   IngredientsMaster = dm.IngredientsMaster,
                                   IngredientsMasterAllergensEntity = ima,
                                   IngredientsCosting = dm.IngredientsCosting,
                               };
            }

            if (cost != null)
            {
                initialQuery = from dm in initialQuery
                               join ic in _athenaDbcontext.IngredientsCosting
                               on dm.IngredientsMaster.IngSk equals ic.IngSk
                               select new
                               {
                                   IngredientsMaster = dm.IngredientsMaster,
                                   IngredientsMasterAllergensEntity = dm.IngredientsMasterAllergensEntity,
                                   IngredientsCosting = ic,
                               };
            }

            var query = initialQuery.AsQueryable();

            if (!string.IsNullOrEmpty(supplierName))
                query = query.Where(x => x.IngredientsMaster.SupplierName == supplierName);


            query = query.Where(i => i.IngredientsMaster.IngredientsName.Contains(ingredientName));


            if (!string.IsNullOrWhiteSpace(supplierReferenceNo))
                query = query.Where(x => x.IngredientsMaster.SupplierReferenceNo == supplierReferenceNo);

            if (reqData != null && reqData.MeasureOptionIds != null && reqData.MeasureOptionIds.Count > 0)
                query = query.Where(x => reqData.MeasureOptionIds.Contains(x.IngredientsMaster.MeasureOptionId));

            if (reqData != null && reqData.SupplierIds != null && reqData.SupplierIds.Count > 0)
                query = query.Where(x => reqData.SupplierIds.Contains(x.IngredientsMaster.SupplierId));

            if (cost != null)
                query = query.Where(x => x.IngredientsCosting.UnitPrice == cost);

            if (reqData != null && reqData.AllergenIds != null && reqData.AllergenIds.Count > 0)
                query = query.Where(x => !reqData.AllergenIds.Contains(x.IngredientsMasterAllergensEntity.AllergenId));

            int skip = recsPerPage * currPageNo;

            List<IngredientsMaster> ingredients = await query.Select(i => i.IngredientsMaster).Skip(skip).Take(recsPerPage).ToListAsync();

            return new IngredientsDbModel
            {
                Ingredients = ingredients,
                TotalIngredients = await query.Select(i => i.IngredientsMaster).CountAsync()
            };
        }

        public async Task<object> Edit(EditIngredientReqModel reqData)
        {
            var ingredient = await _athenaDbcontext.IngredientsMaster.SingleOrDefaultAsync(a => a.IngSk == reqData.IngSK);
            if (ingredient == null)
            {
                throw new Exception("Ingredient not found.");
            }
            ingredient.IngredientsName = reqData.IngredientsName;
            ingredient.CalculationMethod = reqData.PackUnitOfMeasure;
            ingredient.SupplierName = reqData.PreferredSupplier;
            ingredient.MinimumOrderQuantity = reqData.MinimumOrderQuantity;
            ingredient.SpecificationDescription = reqData.SpecificationDescription;
            //ingredient.Cost = reqData.Cost;
            ingredient.SupplierReferenceNo = reqData.SupplierReferenceNo;
            //ingredient.PackSize = reqData.Packsize;
            ingredient.StoreSk = reqData.StoreSk;
            ingredient.SalePrice = reqData.SalePrice;
            ingredient.IsOnMenu = reqData.IsOnMenu;
            ingredient.Weight = reqData.Weight;
            ingredient.McwCode = reqData.McwCode;
            ingredient.CountrySk = reqData.CountrySk;
            ingredient.PrepYield = reqData.PrepYield;
            ingredient.FoodGroupSk = reqData.IngFoodGroupSk;
            ingredient.ModifiedBy = reqData.UserId;
            ingredient.ModifiedDate = DateTime.UtcNow;
            ingredient.GenericYield = reqData.GenericYield;
            ingredient.Barcode = reqData.Barcode;
            ingredient.IngredientTypeId = reqData.IngredientTypeId;
            ingredient.StatusSk = reqData.StatusSk;
            ingredient.ContractStatusSk = reqData.ContractStatusSk;

            await _athenaDbcontext.SaveChangesAsync();

            var ingredientMasterCosting = await _athenaDbcontext.IngredientsCosting.SingleOrDefaultAsync(a => a.IngSk == reqData.IngSK);

            if (ingredientMasterCosting != null)
            {
                ingredientMasterCosting.UnitPrice = reqData.Cost;
                ingredientMasterCosting.PackSize = reqData.Packsize;
                ingredientMasterCosting.PackCost = reqData.PackCost;
                await _athenaDbcontext.SaveChangesAsync();
            }
            else
            {
                IngredientsCosting ingredientsMasterCosting = new IngredientsCosting()
                {
                    IngSk = reqData.IngSK,
                    UnitPrice = reqData.Cost,
                    PackSize = reqData.Packsize,
                    PackCost = reqData.PackCost
                };
                await _athenaDbcontext.IngredientsCosting.AddAsync(ingredientsMasterCosting);
                await _athenaDbcontext.SaveChangesAsync();
            }

            return true;
        }

        public async Task<object> UpdateGenericYield(long ingSk, long genericYield, string userId)
        {
            var ingredient = await _athenaDbcontext.IngredientsMaster.SingleOrDefaultAsync(a => a.IngSk == ingSk);
            if (ingredient == null)
            {
                throw new Exception("Ingredient not found.");
            }

            ingredient.GenericYield = genericYield;
            ingredient.ModifiedBy = userId;
            ingredient.ModifiedDate = DateTime.UtcNow;
            await _athenaDbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<List<IngredientsMaster>> GetExportIngredients(GetExportIngredientsReqModel reqData)
        {
            var query = _athenaDbcontext.IngredientsMaster.AsQueryable();
            if (reqData.SupplierName != null && reqData.SupplierName.Count > 0)
                query = query.Where(x => !string.IsNullOrEmpty(x.SupplierName) && reqData.SupplierName.Contains(x.SupplierName));

            if (reqData.StoreSk != null && reqData.StoreSk.Where(x => x > 0).Count() > 0)
                query = query.Where(x => reqData.StoreSk.Contains(x.StoreSk));

            if (reqData.FoodGroupSk != null && reqData.FoodGroupSk.Where(x => x > 0).Count() > 0)
                query = query.Where(x => x.FoodGroupSk.HasValue && reqData.FoodGroupSk.Contains(x.FoodGroupSk.Value));

            if (reqData.IngredientTypeId != null && reqData.IngredientTypeId.Where(x => x > 0).Count() > 0)
                query = query.Where(x => x.IngredientTypeId.HasValue && reqData.IngredientTypeId.Contains(x.IngredientTypeId.Value));

            query = query.Where(x => x.IsOnMenu == reqData.IsRetailUsage);

            return await query.ToListAsync();
        }

        public async Task<List<IngredientsMaster>> ApplyFilterIngredients(ApplyFilterIngredientsReqModel reqData)
        {
            var query = _athenaDbcontext.IngredientsMaster.AsQueryable();

            if (!string.IsNullOrEmpty(reqData.SupplierReferenceNo))
                query = query.Where(x => x.SupplierReferenceNo != null && reqData.SupplierReferenceNo.Contains(x.SupplierReferenceNo));

            if (reqData.SupplierName != null && reqData.SupplierName.Count > 0)
                query = query.Where(x => !string.IsNullOrEmpty(x.SupplierName) && reqData.SupplierName.Contains(x.SupplierName));

            if (reqData.MinimumOrderQuantity != null && reqData.MinimumOrderQuantity > 0)
                query = query.Where(x => x.MinimumOrderQuantity != null && reqData.MinimumOrderQuantity == x.MinimumOrderQuantity);

            if (reqData.StoreSk != null && reqData.StoreSk.Where(x => x > 0).Count() > 0)
                query = query.Where(x => reqData.StoreSk.Contains(x.StoreSk));

            if (reqData.IngredientTypeId != null && reqData.IngredientTypeId.Where(x => x > 0).Count() > 0)
                query = query.Where(x => x.IngredientTypeId.HasValue && reqData.IngredientTypeId.Contains(x.IngredientTypeId.Value));

            if (reqData.MeasureOptionId != null && reqData.MeasureOptionId.Where(x => x > 0).Count() > 0)
                query = query.Where(x => reqData.MeasureOptionId.Contains(x.MeasureOptionId));

            if (reqData.UnitCost != null && reqData.UnitCost > 0)
                query = query.Where(x => reqData.UnitCost == reqData.UnitCost);

            if (reqData.PackCost != null && reqData.PackCost > 0)
                query = query.Where(x => reqData.PackCost == reqData.PackCost);

            if (reqData.Weight != null && reqData.Weight > 0)
                query = query.Where(x => reqData.Weight == reqData.Weight);

            return await query.ToListAsync();
        }


        public async Task<List<IngredientsMaster>> GetIngredients(List<long> ingSks)
        {
            return await _athenaDbcontext.IngredientsMaster.Where(i => ingSks.Contains(i.IngSk)).ToListAsync();
        }

        #region Reports
        public async Task<object> GetDeclarationHeaderDetails()
        {
            var result = await (from im in _athenaDbcontext.IngredientsMaster
                                join icom in _athenaDbcontext.IngredientsMasterCompositionEntity on im.IngSk equals icom.IngSk into j
                                from icom in j.DefaultIfEmpty()
                                where im.SupplierName != "MCW"
                                group new { im, icom } by im.SupplierName into g
                                select new
                                {
                                    SupplierName = g.Key,
                                    SupplierId = g.Where(x => x.im.SupplierName == g.Key).Select(x => x.im.SupplierId).Distinct().FirstOrDefault(),
                                    MissingCount = g.Count(x => x.icom.Ingredients == null),
                                    ProvidedCount = g.Count(x => x.icom.Ingredients != null),
                                    TotalCount = g.Count()
                                }).ToListAsync();
            return result;
        }

        public async Task<List<DeclarationStausDetailsModel>> GetDeclarationDetails(int supplierId, string status)
        {
            if (status.ToLower() == "missing")
            {
                var query = await (from im in _athenaDbcontext.IngredientsMaster
                                   join icom in _athenaDbcontext.IngredientsMasterCompositionEntity
                                   on im.IngSk equals icom.IngSk into joined
                                   from icom in joined.DefaultIfEmpty()
                                   where im.SupplierName != "MCW" && im.SupplierId == supplierId && icom.Ingredients == null
                                   select new DeclarationStausDetailsModel
                                   {
                                       Ingredient = im,
                                       DeclartaionStatusDescription = icom.Ingredients
                                   }).ToListAsync();

                return query;
            }
            else if (status.ToLower() == "provided")
            {
                var query = await (from im in _athenaDbcontext.IngredientsMaster
                                   join icom in _athenaDbcontext.IngredientsMasterCompositionEntity
                                   on im.IngSk equals icom.IngSk into joined
                                   from icom in joined.DefaultIfEmpty()
                                   where im.SupplierName != "MCW" && im.SupplierId == supplierId && icom.Ingredients != null
                                   select new DeclarationStausDetailsModel
                                   {
                                       Ingredient = im,
                                       DeclartaionStatusDescription = icom.Ingredients
                                   }).ToListAsync();

                return query;
            }
            return null;
        }
        #endregion
    }
}
