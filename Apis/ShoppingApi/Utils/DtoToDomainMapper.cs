using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApi.Model.Domain;
using ShoppingApi.Model.Dto;

namespace ShoppingApi.Utils
{
    public static class DtoToDomainMapper
    {
        public static ShoppingList MapShoppingList(ShoppingListDto shoppingList)
        {
            return new ShoppingList()
            {
                Name = shoppingList.Name,
                ListId = shoppingList.ListId,
                Items = shoppingList.Items.Select(MapShoppingItem).ToList()
            };
        }

        public static ShoppingItem MapShoppingItem(ShoppingItemDto shoppingItem)
        {
            return new ShoppingItem()
            {
                ItemId = shoppingItem.ItemId,
                Name = shoppingItem.Name,
                Quantity = shoppingItem.Quantity,
                ShoppingListId = shoppingItem.ShoppingListId,
                ItemCategory = MapItemCategory(shoppingItem.ItemCategory)
            };
        }

        public static ItemCategory MapItemCategory(ItemCategoryDto itemCategory)
        {
            return new ItemCategory()
            {
                CategoryId = itemCategory.CategoryId,
                Name = itemCategory.Name
            };
        }
    }
}
