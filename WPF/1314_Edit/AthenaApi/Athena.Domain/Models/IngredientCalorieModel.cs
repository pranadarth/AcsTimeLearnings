using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class IngredientCalorieModel
    {
        public int? IngMastCaloricSK { get; set; }
        public string? IngredientName { get; set; }
        public long? IngSk { get; set; }
        public int? CalTypeSk { get; set; }
        public string? CalType { get; set; }
        public string? Unit { get; set; }
        public string? Code { get; set; }
        public double? Value { get; set; }
    }
}
