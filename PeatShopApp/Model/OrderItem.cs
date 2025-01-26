using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeatShopApp.Model
{
    public class OrderItem
    {
        public int OrderId { get; set; }

        // Navigation property for Order
        [ForeignKey(nameof(OrderId))]
        public virtual order Order { get; set; }

        public int ProductId { get; set; }

        // Navigation property for Product
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "numeric(10, 2)")]
        public decimal Price { get; set; }
    }
}