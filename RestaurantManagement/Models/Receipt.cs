using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantManagement.Models
{
    public enum ReceiptStatus { Open, Closed }

    public class Receipt
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime OpenTime { get; set; } = DateTime.Now;
        public DateTime? CloseTime { get; set; }
        public int TableNumber { get; set; }
        public Guid WaiterId { get; set; }           
        public Waiter? Waiter { get; set; }       
        public ReceiptStatus Status { get; set; } = ReceiptStatus.Open;
        public List<Order> Orders { get; set; } = new();

        public decimal TotalAmount => Orders.Sum(o => o.Quantity * (o.MenuItem?.Price ?? 0));
        public decimal TipsAmount => Status == ReceiptStatus.Closed ? TotalAmount * 0.10m : 0; // 10%
    }
}