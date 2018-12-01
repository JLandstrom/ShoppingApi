using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApi.Model.Domain;
using StandardLibrary.Repository;

namespace ShoppingApi.Repository.Interface
{
    public interface IItemCategoryRepository : IRepository<ItemCategory>
    {
    }
}
