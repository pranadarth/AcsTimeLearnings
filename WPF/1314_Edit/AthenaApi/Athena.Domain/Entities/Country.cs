using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Countries")]
    public class Country : BaseEntity
    {

        [Key]
        [Column("Country_Sk")]
        public int CountrySk { get; set; }
        [Column("Ctry_Code")]
        [MaxLength(10)]
        [Required]
        public string CountryCode { get; set; }
        [Column("Ctry_Name")]
        [MaxLength(200)]
        [Required]
        public string CountryName { get; set; }
        [Column("Active_Status")]
        [Required]
        public bool ActiveStatus { get; set; }


        public ICollection<IngredientsMaster> IngredientMaster { get; set; }
    }
}
