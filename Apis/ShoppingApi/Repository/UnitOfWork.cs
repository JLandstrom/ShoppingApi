using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingApi.Data;
using ShoppingApi.Repository.Interface;

namespace ShoppingApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ShoppingContext _dbContext;

        public IShoppingListRepository ShoppingLists { get; }
        public IShoppingItemRepository ShoppingItems { get; }
        public IItemCategoryRepository ItemCategories { get; }
        
        public UnitOfWork(ShoppingContext context,
            IShoppingListRepository shoppingListRepository,
            IShoppingItemRepository shoppingItemRepository,
            IItemCategoryRepository itemCategoryRepository)
        {
            _dbContext = context;
            ShoppingLists   = shoppingListRepository;
            ShoppingItems   = shoppingItemRepository;
            ItemCategories = itemCategoryRepository;
        }

        public void Complete()
        {
            _dbContext.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Rollback()
        {
            //Do nothing
        }

    }
}
