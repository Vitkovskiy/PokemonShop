using System;

namespace PokemonShop.DataTransferObjects
{
    public class OrderHistoryItemDto
    {
        public string UserName { get; set; }
        public string OrderDate { get; set; }
        public int Count { get; set; }
    }
}
