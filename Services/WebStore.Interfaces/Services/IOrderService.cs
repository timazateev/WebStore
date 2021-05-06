using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.DTO;

namespace WebStore.Infrastructure.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName);

        Task<OrderDTO> GetOrderById(int id);

        Task<OrderDTO> CreateOrder(string UserName, CreateOrderModel OrderModel);
    }
}
