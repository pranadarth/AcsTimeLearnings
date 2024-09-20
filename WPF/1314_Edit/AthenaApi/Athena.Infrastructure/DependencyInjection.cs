using Athena.Application.Interface;
using Athena.Application.RepositoryInterface;
using Athena.Application.RepositoryInterface.Reports;
using Athena.Application.Service;
using Athena.Domain.Models;
using Athena.Infrastructure.Database;
using Athena.Infrastructure.Repository;
using Athena.Infrastructure.Repository.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AthenaDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AthenaDb"))
            );

            services.AddScoped<IIngredientsRepository, IngredientsRepository>();

            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<ISalesFinancialRepository, SalesFinancialRepository>();
            services.AddScoped<IProductDetailsRepository, ProductDetailsRepository>();
            services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
            services.AddScoped<IIssueSheetRepository, IssueSheetRepository>();
            services.AddScoped<ISandwichDetailsRepository, SandwichDetailsRepository>();
            services.AddScoped<IMealsByWardRepository, MealByWardRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IIngredientCalorieRepository, IngredientCalorieRepository>();
            services.AddScoped<IIngredientStoreRepository, IngredientStoreRepository>();
            services.AddScoped<IFoodCaloricTypeRepository, FoodCaloricTypeRepository>();
            services.AddScoped<IIngredientAllergenOptionsRepository, IngredientAllergenOptionsRepository>();
            services.AddScoped<IIngredientAllergenOptionsRepository, IngredientAllergenOptionsRepository>();
            services.AddScoped<IIngredientMasterAllergenRepository, IngredientMasterAllergenRepository>();
            services.AddScoped<IAllergenRepository, AllergenRepository>();
            services.AddScoped<ISubAllergenRepository, SubAllergenRepository>();
            services.AddScoped<IIngredientsLinkingRepository, IngredientsLinkingRepository>();

            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<IDishCategoryRepository, DishCategoryRepository>();
            services.AddScoped<IDishFoodTypeRepository, DishFoodTypeRepository>();
            services.AddScoped<IDishHeatTypeRepository, DishHeatTypeRepository>();
            services.AddScoped<IDishMealTypeRepository, DishMealTypeRepository>();
            services.AddScoped<IDishMenuTypeRepository, DishMenuTypeRepository>();
            services.AddScoped<IDishTemplatesRepository, DishTemplatesRepository>();
            services.AddScoped<ILabelTypeRepository, LabelTypeRepository>();
            services.AddScoped<IPortionControlRepository, PortionControlRepository>();

            services.AddScoped<IDishProcessRepository, DishProcessRepository>();
            services.AddScoped<IDishProcessSectionRepository, DishProcessSectionRepository>();
            services.AddScoped<IDishProcessStepRepository, DishProcessStepRepository>();
            services.AddScoped<IDishTemperatureRepository, DishTemperatureRepository>();
            services.AddScoped<IDishTimeRepository, DishTimeRepository>();
            services.AddScoped<IDishPreparationRepository, DishPreparationRepository>();
            services.AddScoped<IDishSubDishRepository, DishSubDishRepository>();
            services.AddScoped<IDishIngredientsRepository, DishIngredientsRepository>();
            services.AddScoped<IDishLabelDetailsRepository, DishLableDetailsRepository>();
            services.AddScoped<IDishCategoryMappingRepository, DishCategoryMappingRepository>();
            services.AddScoped<IDishIngSubstitutionRepository, DishIngSubstitutionRepository>();
            services.AddScoped<IDishMealMappingRepository, DishMealMappingRepository>();
            services.AddScoped<IDishMenuMappingRepository, DishMenuMappingRepository>();
            services.AddScoped<IMenuFormMealCourseMapRepository, MenuFormMealCourseMapRepository>();
            services.AddScoped<IMenuFormTypesRepository, MenuFormTypesRepository>();
            services.AddScoped<IMenuFormAllowanceRepository, MenuFormAllowanceRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<ISubLocationRepository, SubLocationRepository>();
            services.AddScoped<ICutoffTimeRepository, CutoffTimeRepository>();
            services.AddScoped<IMenuFormDetailsRepository, MenuFormDetailsRepository>();
            services.AddScoped<ILocationMenuTypeMappingRepository, LocationMenuTypeMappingRepository>();

            return services;
        }
    }
}
