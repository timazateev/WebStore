using System;
using System.Collections.Generic;
using WebStore.Domain.ViewModels;

namespace WebStore.Domain.DTO
{

    /// <summary>
    /// Iformation about order
    /// </summary>
    public class OrderDTO
    {
        /// <summary>
        /// Identificator
        /// </summary>
        public int id { get; set; }
        
        /// <summary>
        /// Order name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Phone number from order for connection
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// Delivery address 
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Order's time and date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Order items
        /// </summary>
        public IEnumerable<OrderItemDTO> Items { get; set; }
    }

    public class OrderItemDTO
    {
        public int id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateOrderModel
    {
        public OrderViewModel Order { get; set; }

        public IList<OrderItemDTO> Items { get; set; }
    }
}