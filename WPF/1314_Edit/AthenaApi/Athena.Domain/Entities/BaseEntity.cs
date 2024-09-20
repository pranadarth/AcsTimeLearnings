using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Column("Created_By")]
        [MaxLength(50)]
        [Required]
        public string CreatedBy { get; set; }
        [Column("Created_Date")]
        [Required]
        public System.DateTime CreatedDate { get; set; }
        [Column("Modified_By")]
        [MaxLength(50)]
        public string? ModifiedBy { get; set; }
        [Column("Modified_Date")]
        public System.DateTime? ModifiedDate { get; set; }
    }
}
