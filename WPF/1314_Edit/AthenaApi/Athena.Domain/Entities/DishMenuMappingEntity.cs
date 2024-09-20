using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Dish_Menu_Mapping")]
    public class DishMenuMappingEntity
    {
        [Key]
        [Column("Dish_Menu_Mapping_Sk")]
        public int DishMenuMappingSk { get; set; }

        [ForeignKey("DishManager")]
        [Column("Dish_Sk")]
        public int? DishSk { get; set; }

        [ForeignKey("DishMenuType")]
        [Column("Dish_MenuType_Id")]
        public int? DishMenuTypeId { get; set; }

        [Column("Active_Status")]
        public bool ActiveStatus { get; set; } = true;

        [Column("SysStartTime")]
        public DateTime SysStartTime { get; set; }

        [Column("SysEndTime")]
        public DateTime SysEndTime { get; set; }

        // Navigation properties
        public virtual DishMenuTypeEntity DishMenuType { get; set; }
        public virtual DishManagerEntity DishManager { get; set; }
    }
}
