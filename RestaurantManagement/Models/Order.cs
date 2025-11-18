using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace RestaurantManagement.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // К какому чеку относится заказ
        public Guid ReceiptId { get; set; }

        // Какая именно страва заказана
        public Guid MenuItemId { get; set; }

        // Удобная ссылка на саму страву (чтобы не искать каждый раз)
        public MenuItem? MenuItem { get; set; }

        // Сколько порций
        public int Quantity { get; set; } = 1;
    }
}