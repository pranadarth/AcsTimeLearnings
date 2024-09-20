using Athena.Application.Interface;
using Athena.Application.Interface.Reports;
using Athena.Application.RepositoryInterface;
using Athena.Application.Service;
using Athena.Application.Service.Reports;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDependency(this IServiceCollection services)
        {
            services.AddScoped<IIngredientsService, IngredientsService>();

            services.AddScoped<IReportService, ReportService>();

            services.AddScoped<ISalesFinancialService, SalesFinancialService>();
            services.AddScoped<IProductDetailsService, ProductDetailsService>();
            services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            services.AddScoped<IIssueSheetService, IssueSheetService>();
            services.AddScoped<ISandwichDetailsService, SandwichDetailsService>();
            services.AddScoped<IMealsByWardService, MealByWardService>();
            services.AddSingleton<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IIngredientCalorieService, IngredientCalorieService>();
            services.AddScoped<IIngredientStoreService, IngredientStoreService>();
            services.AddScoped<IIngredientAllergenService, IngredientAllergenService>();
            services.AddScoped<IDishService, DishService>();
            services.AddScoped<IMenuFormSevice, MenuFormSevice>();
            return services;
        }
    }
}
