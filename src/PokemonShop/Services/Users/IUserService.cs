using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonShop.Domain.Models;

namespace PokemonShop.Services.Users
{
    public interface IUserService
    {
        User CreateUser(string name, string email, string phoneName);
    }
}
