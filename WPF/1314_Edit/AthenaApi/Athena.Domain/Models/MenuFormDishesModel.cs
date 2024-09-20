using Athena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class MenuFormDishesModel
    {
        public int? Allowance { get; set; }
        public int CourseTypeSk { get; set; }
        public string CourseName { get; set; }
        public List<DishMenuFormModel> Dish { get; set; } = new List<DishMenuFormModel>();
    }

    public class DishMenuFormModel
    {
        public int DishSk {  get; set; }
        public string DishName { get; set; }
        public string Display_Name { get; set;}
        public string Display_Description { get; set;}
        public float cost_per_portion { get; set; }
        public float cost { get; set; }
        public float sale_price { get; set; }
        public string Dish_Image { get; set; }
    }
}
