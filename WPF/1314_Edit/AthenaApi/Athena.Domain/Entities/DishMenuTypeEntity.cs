using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Dish_Menu_Type")]
    public class DishMenuTypeEntity
    {
        [Key]
        [Column("Dish_MenuType_Id")]
        public int DishMenuTypeId { get; set; }

        [Column("Dish_MenuType_Code")]
        [MaxLength(25)]
        public string? DishMenuTypeCode { get; set; }

        [Column("Dish_MenuType_Desc")]
        [MaxLength(50)]
        public string? DishMenuTypeDesc { get; set; }

        [Column("Display_Order")]
        public int? DisplayOrder { get; set; }

        [Required]
        [Column("Active_Status")]
        public bool ActiveStatus { get; set; }
    }
}
