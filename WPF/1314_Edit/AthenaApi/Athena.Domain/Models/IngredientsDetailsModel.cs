using Athena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class IngredientsDetailsModel
    {
        public List<Ingredients>? Ingredients { get; set; }
        public long TotalIngredients { get; set; }
    }

    public class Ingredients
    {
        public long IngSK { get; set; }
        public string? IngredientsName { get; set; }

        public string? CalculationMethod { get; set; }
        public string? PreferredSupplier { get; set; }
        public float? MinimumOrderQuantity { get; set; }
        public string? SpecificationDescription { get; set; }
        public string? SupplierReferenceNo { get; set; }
        public string? Packsize { get; set; }
        public int Store { get; set; }
        public float Weight { get; set; }
        public int? CountrySk { get; set; }
        public string? Country { get; set; }
        public double? PrepYield { get; set; }
        public int? IngFoodGroupSk { get; set; }
        public string? IngFoodGroup { get; set; }
        public double? GenericYield { get; set; }
        public int? StatusSk { get; set; }
        public string? Status { get; set; }
        public int? ContractStatusSk { get; set; }
        public string? ContractStatus { get; set; }
        public float Cost { get; set; }
        public string? Barcode { get; set; }
        public float? SalePrice { get; set; }
        public int? IngredientTypeId { get; set; }
        public int MearureOptionId { get; set; }
        public bool? IsOnMenu { get; set; }
        public string DeclarationStatus { get; set; }

        public bool IsLinkedWithOtherIng { get; set; }
    }
}
