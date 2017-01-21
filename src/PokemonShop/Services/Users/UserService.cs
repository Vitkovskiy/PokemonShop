using System;
using PokemonShop.Domain.Models;
using PokemonShop.Repository;

namespace PokemonShop.Services.Users
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly IRepository<User> _userRepository;

        #endregion

        #region Ctor

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion

        // create new user
        public User CreateUser(string name, string email, string phoneNumber)
        {
            var user = new User
            {
                Email = email,
                Name = name,
                PhoneNumber = phoneNumber
            };

            _userRepository.Insert(user);

            return user;
        }

        public void UpdateUser(int userId, string name, string phoneNumber)
        {
            var user = _userRepository.Get(userId);

            user.Name = name;
            user.PhoneNumber = phoneNumber;

            _userRepository.Update(user);
        }
    }
}