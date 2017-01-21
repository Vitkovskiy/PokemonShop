using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonShop.DataTransferObjects
{
    public class OrderDto
    {
        [Required, MaxLength(20)]
        public string UserName { get; set; }
        [Required, MaxLength(100)]
        public string Email { get; set; }
        [Required, MaxLength(10)]
        public string PhoneNumber { get; set; }
    }
}
