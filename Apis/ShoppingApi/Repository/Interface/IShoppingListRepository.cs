using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApi.Model.Domain;
using StandardLibrary.Repository;

namespace ShoppingApi.Repository.Interface
{
    public interface IShoppingListRepository : IRepository<ShoppingList>
    {
        List<ShoppingList> GetShoppingListsWithItems();
        ShoppingList GetShoppingListWithItems(int id);
        Task<List<ShoppingList>> GetShoppingListsWithItemsAsync();
        Task<ShoppingList> GetShoppingListWithItemsAsync(int id);
    }
}
