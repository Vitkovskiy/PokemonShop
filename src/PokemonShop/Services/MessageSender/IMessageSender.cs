using System.Threading.Tasks;
using PokemonShop.DataTransferObjects;

namespace PokemonShop.Services.MessageSender
{
    public interface IMessageSender
    {
        Task PokemonOrdered(UserDto user, string subject, string message);
    }
}
