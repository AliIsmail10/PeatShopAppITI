using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeatShopApp.Model
{
    public class order
    {
        [Key]
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        [Column(TypeName = "numeric(10, 2)")]
        public decimal TotalAmount { get; set; }

        public int CustomerId { get; set; }

        // Navigation property for Customer
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; }

        // Navigation property for OrderItems
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}