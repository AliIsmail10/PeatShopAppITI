using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PeatShopApp.Model
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public long Phone { get; set; }

        // Navigation property for Orders
        public virtual ICollection<order> Orders { get; set; } = new List<order>();

        // Navigation property for User
        public virtual User User { get; set; }
    }
}