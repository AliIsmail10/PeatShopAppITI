using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeatShopApp.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Column(TypeName = "numeric(10,2)")]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        // Navigation property for OrderItems
        public string ImagePath { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}