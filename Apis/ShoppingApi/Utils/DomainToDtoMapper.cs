using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApi.Model.Domain;
using ShoppingApi.Model.Dto;

namespace ShoppingApi.Utils
{
    public static class DomainToDtoMapper
    {
        public static ShoppingListDto MapShoppingList(ShoppingList shoppingList)
        {
            return new ShoppingListDto()
            {
                Name = shoppingList.Name,
                ListId = shoppingList.ListId,
                Status = shoppingList.Status,
                Items = shoppingList.Items?.Select(DomainToDtoMapper.MapShoppingItem).ToList()
            };
        }

        public static ShoppingItemDto MapShoppingItem(ShoppingItem shoppingItem)
        {
            return new ShoppingItemDto()
            {
                ItemId = shoppingItem.ItemId,
                Name = shoppingItem.Name,
                Quantity = shoppingItem.Quantity,
                UnitOfMeasure = shoppingItem.UnitOfMeasure,
                ShoppingListId = shoppingItem.ShoppingListId,
                ItemCategory = MapItemCategory(shoppingItem.ItemCategory)
            };
        }

        public static ItemCategoryDto MapItemCategory(ItemCategory itemCategory)
        {
            if (itemCategory == null) return null;
            return new ItemCategoryDto()
            {
                CategoryId = itemCategory.CategoryId,
                Name = itemCategory.Name
            };
        }
    }
}
