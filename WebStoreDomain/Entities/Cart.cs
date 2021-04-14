using System.Collections.Generic;
using System.Linq;

namespace WebStoreDomain.Entities
{
    public class Cart
    {
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

        public int ItemCount => Items?.Sum(item => item.Quantity) ?? 0;
    }

    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
