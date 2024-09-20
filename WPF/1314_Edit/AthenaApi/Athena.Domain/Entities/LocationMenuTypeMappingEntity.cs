using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Location_Menu_Type_Mapping")]
    public class LocationMenuTypeMappingEntity
    {
        [Key]
        [Column("LocMenuMap_Id")]
        public int LocMenuMapId { get; set; }

        [ForeignKey("Location")]
        [Column("Location_Sk")]
        public int? LocationSk { get; set; }

        [ForeignKey("SubLocation")]
        [Column("Sub_Location_Sk")]
        public int? SubLocationSk { get; set; }

        [ForeignKey("DishMenuType")]
        [Column("Dish_MenuType_Id")]
        public int? DishMenuTypeId { get; set; }

        [Column("Active_Status")]
        public bool ActiveStatus { get; set; } = true;

        [Required]
        [Column("Created_By")]
        public string CreatedBy { get; set; } = "SYSTEM";

        [Required]
        [Column("Created_Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column("Modified_By")]
        public string? ModifiedBy { get; set; }

        [Column("Modified_Date")]
        public DateTime? ModifiedDate { get; set; }

        public LocationEntity Location { get; set; }
        public SubLocationEntity SubLocation { get; set; }
        public DishMenuTypeEntity DishMenuType { get; set; }
    }
}
