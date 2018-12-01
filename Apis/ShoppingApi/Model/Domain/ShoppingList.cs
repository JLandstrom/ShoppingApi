using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Model.Domain
{
    public class ShoppingList
    {
        public int ListId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

        public virtual ICollection<ShoppingItem> Items { get; set; }
    }
}
