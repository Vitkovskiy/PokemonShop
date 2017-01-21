
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using PokemonShop.Controllers.WebApi;
using PokemonShop.DataTransferObjects;
using PokemonShop.Domain.Models;
using PokemonShop.Repository;
using PokemonShop.Services.MessageSender;
using PokemonShop.Services.Orders;
using Xunit;
using PokemonShop.Services.Users;

namespace PokemonShop.Tests
{
    public class OrderTests
    {
        [Fact]
        public void Make_new_order_for_new_user()
        {
            var orders = new List<Order>();
            var users = new List<User>();
            var orderDetails = new OrderDto { Email = "we@web.com", PhoneNumber = "0632030959", UserName = "Игорь" };

            var orderRepoMock = new Mock<IRepository<Order>>();
            orderRepoMock.Setup(i => i.Insert(It.IsAny<Order>())).Callback((Order order) =>
            {
                order.Id = 1;
                orders.Add(order);
            });
            orderRepoMock.Setup(a => a.Set).Returns(orders.AsQueryable());

            var userRepoMock = new Mock<IRepository<User>>();
            userRepoMock.Setup(i => i.Insert(It.IsAny<User>())).Callback((User user) => {
                user.Id = 1;
                users.Add(user);
            });
            userRepoMock.Setup(a => a.Set).Returns(users.AsQueryable());

            var orderService = new OrderService(userRepoMock.Object, orderRepoMock.Object);
            var userService = new UserService(userRepoMock.Object);
            var orderController = new OrderController(orderService, userService, new EmailMessageSender(new OptionsWrapper<MailConfiguration>(new MailConfiguration())), userRepoMock.Object);

            orderController.Post(orderDetails);

            Assert.Equal(userRepoMock.Object.Set.Count(), 1); // создан один пользователь
            Assert.Equal(orderRepoMock.Object.Set.Count(), 1); // создан один заказ
            Assert.Equal(orderRepoMock.Object.Set.First().UserId, userRepoMock.Object.Set.First().Id); // айди пользователя в заказе совпадает с айди созданного пользователя
        }

        [Fact]
        public void Correct_user_orders_count()
        {
            var users = (new List<User>()
            {
                new User()
                {
                    Email = "user@user.com",
                    Id = 1,
                    Name = "Игорь",
                    PhoneNumber = "0632030959",
                    Orders = new List<Order>
                    {
                        new Order
                        {
                            Id = 1,
                            OrderDate = DateTime.Now.AddHours(1),
                            UserId = 1
                        },
                        new Order
                        {
                            Id = 2,
                            OrderDate = DateTime.Now.AddHours(2),
                            UserId = 1
                        },
                        new Order
                        {
                            Id = 3,
                            OrderDate = DateTime.Now.AddHours(3),
                            UserId = 1
                        },
                    }
                }
            }).AsQueryable();

            var userRepoMock = new Mock<IRepository<User>>();
            userRepoMock.Setup(a => a.Set).Returns(users);

            var orderRepoMock = new Mock<IRepository<Order>>();
            orderRepoMock.Setup(a => a.Set).Returns(users.SelectMany(x => x.Orders));

            var orderService = new OrderService(userRepoMock.Object, orderRepoMock.Object);
            var orderHistoryItems = orderService.GetOrderHistoryItems();

            Assert.Equal(orderHistoryItems.Count, 1); // один пользователь
            Assert.Equal(orderHistoryItems.First().Count, 3); // с тремя заказами
        } 
    }
}
