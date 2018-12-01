using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ShoppingApi.Repository.Interface
{
    public interface IUnitOfWork
    {
        IShoppingListRepository ShoppingLists { get; }
        IShoppingItemRepository ShoppingItems { get; }
        IItemCategoryRepository ItemCategories { get; }

        void Complete();
        Task CompleteAsync();
        void Rollback();
    }
}
