using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Model.Domain
{
    public class ShoppingItem
    {
        public int ItemId { get; set; }
        public int ShoppingListId { get; set; }
        public int ItemCategoryId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string UnitOfMeasure { get; set; }

        public virtual ShoppingList ShoppingList { get; set; }
        public virtual ItemCategory ItemCategory { get; set; }
    }
}
