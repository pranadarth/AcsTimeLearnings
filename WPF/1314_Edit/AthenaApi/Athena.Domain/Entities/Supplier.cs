using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Suppliers")]
    public class Supplier : BaseEntity
    {
        [Key]
        [Column("Supplier_Id")]
        public int SupplierId { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string Code { get; set; }
        [MaxLength(100)]
        public string? SupplyJersey { get; set; }
        [MaxLength(255)]
        public string? ContactName { get; set; }
        [MaxLength(255)]
        public string? ContactTitle { get; set; }
        [MaxLength(255)]
        public string? Address1 { get; set; }
        [MaxLength(255)]
        public string? Address2 { get; set; }
        [MaxLength(255)]
        public string? Address3 { get; set; }
        [MaxLength(255)]
        public string? PostalCode { get; set; }
        [MaxLength(255)]
        public string? PhoneNumber { get; set; }
        [MaxLength(255)]
        public string? FaxNumber { get; set; }
        [MaxLength]
        public string? Note { get; set; }
        [Column("Active_Status")]
        [Required]
        public bool ActiveStatus { get; set; }
        //public ICollection<IngredientMaster> IngredientMaster { get; set; }
    }
}
