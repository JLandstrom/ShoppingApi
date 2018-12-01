using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingApi.Data;
using ShoppingApi.Model.Domain;
using ShoppingApi.Repository.Interface;
using StandardLibrary.Repository;

namespace ShoppingApi.Repository
{
    public class ShoppingListRepository : GenericEfRepository<ShoppingList>, IShoppingListRepository
    {
        protected ShoppingContext Context => (ShoppingContext)DbContext;

        public ShoppingListRepository(ShoppingContext dbContext) : base(dbContext)
        {
        }

        public List<ShoppingList> GetShoppingListsWithItems()
        {
            return Context.ShoppingLists.Include(sl => sl.Items).ThenInclude(si => si.ItemCategory).ToList();
        }

        public ShoppingList GetShoppingListWithItems(int id)
        {
            var list = Context.ShoppingLists.FirstOrDefault(sl => sl.ListId == id);
            if (list != null)
            {
                list.Items = Context.ShoppingItems.Where(si => si.ShoppingListId == list.ListId)
                    .Include(si => si.ItemCategory).ToList();
                
            }
            return list;
        }

        public async Task<List<ShoppingList>> GetShoppingListsWithItemsAsync()
        {
            return await Context.ShoppingLists.Include(sl => sl.Items).ThenInclude(si => si.ItemCategory).ToListAsync();
        }

        public async Task<ShoppingList> GetShoppingListWithItemsAsync(int id)
        {
            var list = await Context.ShoppingLists.FirstOrDefaultAsync(sl => sl.ListId == id);
            list.Items = await Context.ShoppingItems.Where(si => si.ShoppingListId == list.ListId).Include(si => si.ItemCategory).ToListAsync();
            return list;

        }
    }
}
