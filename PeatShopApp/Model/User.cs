using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeatShopApp.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Role { get; set; }

        public int? CustomerId { get; set; }

        // Navigation property for Customer
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; }
    }
}