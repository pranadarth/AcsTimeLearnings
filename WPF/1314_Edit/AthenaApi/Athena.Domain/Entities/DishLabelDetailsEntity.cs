using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Dish_Label_Details")]
    public class DishLabelDetailsEntity
    {
        [Key]
        [Column("DishLabels_Sk")]
        public int DishLabelsSk { get; set; }

        [Required]
        [Column("Dish_Sk")]
        public int DishSk { get; set; }

        [Required]
        [Column("Label_Type_Id")]
        public int LabelTypeId { get; set; }
        public System.DateTime? SysStartTime { get; set; }
        public System.DateTime? SysEndTime { get; set; }
    }
}
