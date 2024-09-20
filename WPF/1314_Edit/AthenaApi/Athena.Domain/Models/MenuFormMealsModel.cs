using Athena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class MenuFormMealsModel
    {
        public int? MenuFormTypeSk { get; set; }
        public int? MealTypeId { get; set; }
        public int? CourseTypeSk { get; set; }
        public int? Display_Order { get; set; }
        public int? LocationSk { get; set; }
        public int? SubLocationSk { get; set; }
        public MenuFormDetailsEntity? MenuFormDetails { get; set; }
    }

    public class MenuFormDetails
    {
        public int LocMenuMapId { get; set; }
        public int DishCategoryId { get; set; }
        public int DishSk { get; set; }
        public int Dish_MenuType_Id { get; set; }
        public int Quantity { get; set; }
    }
}
