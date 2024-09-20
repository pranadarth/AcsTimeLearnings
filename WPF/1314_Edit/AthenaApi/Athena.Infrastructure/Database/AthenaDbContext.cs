using Athena.Domain.Entities;
using Athena.Domain.Models;
using Athena.Infrastructure.Database.Cache.ApacheIgnite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace Athena.Infrastructure.Database
{
    public class AthenaDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AthenaDbContext(DbContextOptions<AthenaDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;

            string? clientId = GetClientId();
            if (!string.IsNullOrEmpty(clientId))
            {
                string strConnString = this.Database.GetDbConnection().ConnectionString;
                strConnString = strConnString.Replace("{CLIENTID}", "" + clientId + "");

                this.Database.SetConnectionString(strConnString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DishPreparationEntity>(entity =>
            {
                entity.Property(p => p.SysEndTime)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.SysStartTime)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<IngredientsMasterCompositionEntity>(entity =>
            {
                entity.Property(p => p.SysEndTime)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.SysStartTime)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<IngredientsCosting>(entity =>
            {
                entity.Property(p => p.SysEndTime)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.SysStartTime)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<IngredientsMaster>(entity =>
            {
                entity.Property(p => p.SysEndTime)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.SysStartTime)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<DishIngredientEntity>(entity =>
            {
                entity.Property(p => p.SysEndTime)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.SysStartTime)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<DishSubDishEntity>(entity =>
            {
                entity.Property(p => p.SysEndTime)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.SysStartTime)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<DishManagerEntity>(entity =>
            {
                entity.Property(p => p.SysEndTime)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.SysStartTime)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<DishLabelDetailsEntity>(entity =>
            {
                entity.Property(p => p.SysEndTime)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.SysStartTime)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<DishPreparationEntity>(entity =>
            {
                entity.Property(p => p.SysEndTime)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.SysStartTime)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<DishCategoryMappingEntity>(entity =>
            {
                entity.Property(p => p.SysEndTime)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.SysStartTime)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<IngredientsLinkingEntity>(entity =>
            {
                entity.Property(p => p.SysEndTime)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.SysStartTime)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<DishMealMappingEntity>(entity =>
            {
                entity.Property(p => p.SysEndTime)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.SysStartTime)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<DishMenuMappingEntity>(entity =>
            {
                entity.Property(p => p.SysEndTime)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.SysStartTime)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<MenuFormDetailsEntity>(entity =>
            {
                entity.Property(p => p.SysEndTime)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.SysStartTime)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<MenuFormMealCourseMappingEntity>(entity =>
            {
                entity.Property(p => p.SysEndTime)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.SysStartTime)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<MenuFormAllowanceEntity>(entity =>
            {
                entity.Property(p => p.SysEndTime)
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(p => p.SysStartTime)
                    .ValueGeneratedOnAddOrUpdate();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string? clientId = GetClientId();

            if (!string.IsNullOrEmpty(clientId))
            {
                string? strConnString = _configuration.GetConnectionString("AthenaDb");

                if (!string.IsNullOrEmpty(strConnString))
                {
                    strConnString = strConnString.Replace("{CLIENTID}", "" + clientId + "");

                    optionsBuilder.UseSqlServer(strConnString);
                }
            }
        }

        internal string? GetClientId()
        {
            //TODO : Rewrite clientid retreival logic
            string? clientId = string.Empty;
            if (System.Web.HttpContext.Current?.Request.QueryString != null && System.Web.HttpContext.Current.Request.QueryString.Count > 0)
                clientId = System.Web.HttpContext.Current.Request.QueryString.Get("clientId")?.ToString();


            List<string>? allowedClients = _configuration.GetSection("AppSettings:AlowedClients").Get<List<string>>();
            if (allowedClients == null || allowedClients.Count < 1)
                throw new UnauthorizedAccessException();

            if (string.IsNullOrWhiteSpace(clientId))
            {
                string? absolutePath = System.Web.HttpContext.Current?.Request.Url.AbsolutePath.ToString();
                if (!string.IsNullOrWhiteSpace(absolutePath))
                {
                    clientId = allowedClients.Where(x => absolutePath.Contains(x)).SingleOrDefault();
                }
            }

            return clientId;
        }

        public DbSet<IngredientsMaster> IngredientsMaster { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<LoginTracker> LoginTracker { get; set; }
        public DbSet<MeasureOptions> MeasureOptions { get; set; }
        public DbSet<IngredientStore> IngredientStore { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<FoodGroup> FoodGroup { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<IngredientStatus> IngredientStatus { get; set; }
        public DbSet<IngredientContractStatus> IngredientContractStatus { get; set; }
        public DbSet<IngredientsMasterCaloric> IngredientsMasterCaloric { get; set; }
        public DbSet<IngredientsCosting> IngredientsCosting { get; set; }
        public DbSet<IngredientTypeEntity> IngredientTypeEntity { get; set; }
        public DbSet<FoodCaloricTypeEntity> FoodCaloricTypeEntity { get; set; }

        public DbSet<AllergenOptionMappingEntity> AllergenOptionMappingEntity { get; set; }
        public DbSet<AllergenOptionEntity> AllergenOptionEntity { get; set; }
        public DbSet<AllergenEntity> AllergenEntity { get; set; }
        public DbSet<IngredientsMasterAllergensEntity> IngredientsMasterAllergensEntity { get; set; }
        public DbSet<SubAllergensEntity> SubAllergensEntity { get; set; }
        public DbSet<IngredientsMasterCompositionEntity> IngredientsMasterCompositionEntity { get; set; }
        public DbSet<IngredientsLinkingEntity> IngredientsLinkingEntity { get; set; }

        //Dish
        public DbSet<DishCategoryEntity> DishCategoryEntity { get; set; }
        public DbSet<DishFoodTypeEntity> DishFoodTypeEntity { get; set; }
        public DbSet<DishHeatTypeEntity> DishHeatTypeEntity { get; set; }
        public DbSet<DishLabelDetailsEntity> DishLabelDetailsEntity { get; set; }
        public DbSet<DishManagerEntity> DishManagerEntity { get; set; }
        public DbSet<DishMealTypeEntity> DishMealTypeEntity { get; set; }
        public DbSet<DishMenuTypeEntity> DishMenuTypeEntity { get; set; }
        public DbSet<DishTemplateEntity> DishTemplateEntity { get; set; }
        public DbSet<LabelTypeEntity> LabelTypeEntity { get; set; }
        public DbSet<PortionControlEntity> PortionControlEntity { get; set; }

        public DbSet<DishProcessEntity> DishProcessEntity { get; set; }
        public DbSet<DishProcessSectionEntity> DishProcessSectionEntity { get; set; }
        public DbSet<DishProcessStepEntity> DishProcessStepEntity { get; set; }
        public DbSet<DishTimeEntity> DishTimeEntity { get; set; }
        public DbSet<DishTemperatureEntity> DishTemperatureEntity { get; set; }
        public DbSet<DishPreparationEntity> DishPreparationEntity { get; set; }
        public DbSet<DishIngredientEntity> DishIngredientEntity { get; set; }
        public DbSet<DishSubDishEntity> DishSubDishEntity { get; set; }
        public DbSet<DishCategoryMappingEntity> DishCategoryMappingEntity { get; set; }
        public DbSet<DishSubstitutionDetailsEntity> DishSubstitutionDetailsEntity { get; set; }

        public DbSet<LocationEntity> LocationEntity { get; set; }
        public DbSet<SubLocationEntity> SubLocationEntity { get; set; }
        public DbSet<LocationMenuTypeMappingEntity> LocationMenuTypeMappingEntity { get; set; }
        public DbSet<DishMenuMappingEntity> DishMenuMappingEntity { get; set; }
        public DbSet<DishMealMappingEntity> DishMealMappingEntity { get; set; }
        public DbSet<MenuFormTypeEntity> MenuFormTypeEntity { get; set; }
        public DbSet<CutoffTimeEntity> CutoffTimeEntity { get; set; }

        public DbSet<CourseTypesEntity> CourseTypesEntity { get; set; }
        public DbSet<MenuFormDetailsEntity> MenuFormDetailsEntity { get; set; }
        public DbSet<MenuFormMealCourseMappingEntity> MenuFormMealCourseMappingEntity { get; set; }
        public DbSet<MenuFormAllowanceEntity> MenuFormAllowanceEntity { get; set; }
    }
}