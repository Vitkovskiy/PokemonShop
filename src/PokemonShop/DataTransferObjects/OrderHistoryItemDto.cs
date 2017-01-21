using System;

namespace PokemonShop.DataTransferObjects
{
    public class OrderHistoryItemDto
    {
        public string UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public int Count { get; set; }
    }
}
