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
    public class ItemCategoryRepository : GenericEfRepository<ItemCategory>, IItemCategoryRepository
    {
        public ItemCategoryRepository(ShoppingContext dbContext) : base(dbContext)
        {
        }

        protected ShoppingContext Context => (ShoppingContext)DbContext;
    }
}
