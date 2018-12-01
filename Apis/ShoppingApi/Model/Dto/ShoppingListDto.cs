using System.Collections.Generic;

namespace ShoppingApi.Model.Dto
{
    public class ShoppingListDto
    {
        public int ListId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

        public virtual List<ShoppingItemDto> Items { get; set; }
    }
}