using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonShop.Domain.Models
{
    // Represents a user
    public class User : BaseEntity
    {
        [MaxLength(20), Required]
        public string Name { get; set; }
        [MaxLength(100), Required]
        public string Email { get; set; }
        [MaxLength(10), Required]
        public string PhoneNumber { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
