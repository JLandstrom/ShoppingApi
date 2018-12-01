using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApi.Model.Domain;

namespace ShoppingApi.Model.Dto
{
    public class ShoppingItemDto
    {
        public int ItemId { get; set; }
        public int ShoppingListId { get; set; }
        public int ItemCategoryId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public ItemCategoryDto ItemCategory { get; set; }
    }
}
