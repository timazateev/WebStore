using System.Linq;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities.Orders;

namespace WebStore.Services.Mapping
{
    public static class OrderMapper
    {
        public static OrderItemDTO ToDTO(this OrderItem Item) => Item is null
            ? null
            : new OrderItemDTO
            {
                id = Item.id,
                Price = Item.Price,
                Quantity = Item.Quantity,
            };

        public static OrderItem FromDTO(this OrderItemDTO Item) => Item is null
            ? null
            : new OrderItem
            {
                id = Item.id,
                Price = Item.Price,
                Quantity = Item.Quantity,
            };

        public static OrderDTO ToDTO(this Order Order) => Order is null
            ? null
            : new OrderDTO
            {
                id = Order.id,
                Name = Order.Name,
                Address = Order.Address,
                Phone = Order.Phone,
                Date = Order.Date,
                Items = Order.Items.Select(ToDTO)
            };

        public static Order FromDTO(this OrderDTO Order) => Order is null
            ? null
            : new Order
            {
                id = Order.id,
                Name = Order.Name,
                Address = Order.Address,
                Phone = Order.Phone,
                Date = Order.Date,
                Items = Order.Items.Select(FromDTO).ToList()
            };
    }
}