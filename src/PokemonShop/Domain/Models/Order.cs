using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonShop.Domain.Models
{
    // Represents a order
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
