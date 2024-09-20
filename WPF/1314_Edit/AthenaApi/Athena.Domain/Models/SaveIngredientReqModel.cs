using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class SaveIngredientReqModel
    {
        public string IngredientsName { get; set; }
        public string? PackUnitOfMeasure { get; set; }
        public string? SupplierName { get; set; }
        public float? MinimumOrderQuantity { get; set; }
        public string? SpecificationDescription { get; set; }
        public int SupplierId { get; set; }
        public string? SupplierReferenceNo { get; set; }
        public int StoreSk { get; set; }
        public float? SalePrice { get; set; }
        public bool? IsOnMenu { get; set; }
        public int MeasureOptionId { get; set; }
        public float Weight { get; set; }
        public long? McwCode { get; set; }
        public int? CountrySk { get; set; }
        public double? PrepYield { get; set; }
        public int? FoodGroupSk { get; set; }
        public double? GenericYield { get; set; }
        public string? Barcode { get; set; }
        public int IngredientTypeId { get; set; }
        public int StatusSk { get; set; }
        public int? ContractStatusSk { get; set; }
        public string UserId { get; set; }
        public float Cost { get; set; }
        public string? PackSize { get; set; }
        public float? PackCost { get; set; }
    }
}
