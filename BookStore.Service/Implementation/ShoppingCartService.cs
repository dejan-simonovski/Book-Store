
using BookStore.Domain.Domain;
using BookStore.Domain.DTO;
using BookStore.Repository.Interface;
using BookStore.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<BookPublisher> _productRepository;
        private readonly IRepository<Order> _orderRepository;
       private readonly IRepository<BookPublisherInOrder> _productInOrderRepository;

        public ShoppingCartService(IUserRepository userRepository, IRepository<ShoppingCart> shoppingCartRepository, IRepository<BookPublisher> productRepository, IRepository<Order> orderRepository, IRepository<BookPublisherInOrder> productInOrderRepository)
        {
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _productInOrderRepository = productInOrderRepository;
        }

        public ShoppingCart AddProductToShoppingCart(string userId, AddToCartDTO model)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userCart = loggedInUser?.UserCart;

                var selectedProduct = _productRepository.Get(model.SelectedProductId);

                if (selectedProduct != null && userCart != null)
                {
                    userCart?.BookInCart?.Add(new BookPublisherInCart
                    {
                        BookPublisher = selectedProduct,
                        BookPublisherId = selectedProduct.Id,
                        ShoppingCart = userCart,
                        ShoppingCartId = userCart.Id,
                        Quantity = model.Quantity
                    });

                    return _shoppingCartRepository.Update(userCart);
                }
            }
            return null;
        }

        public bool deleteFromShoppingCart(string userId, Guid? Id)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);


                var product_to_delete = loggedInUser?.UserCart?.BookInCart.First(z => z.BookPublisherId == Id);

                loggedInUser?.UserCart?.BookInCart?.Remove(product_to_delete);

                _shoppingCartRepository.Update(loggedInUser.UserCart);

                return true;

            }

            return false;
        }

        public AddToCartDTO getProductInfo(Guid Id)
        {
            var selectedProduct = _productRepository.Get(Id);
            if (selectedProduct != null)
            {
                var model = new AddToCartDTO
                {
                    SelectedProductName = selectedProduct.Book.Title,
                    SelectedProductId = selectedProduct.Id,
                    Quantity = 1
                };
                return model;
            }
            return null;
        }

        public ShoppingCart getShoppingCartDetails(string userId)
        {
            if (userId != null && !userId.IsNullOrEmpty())
            {
                var loggedInUser = _userRepository.Get(userId);

                var allProducts = loggedInUser?.UserCart?.BookInCart?.ToList();

                var totalPrice = 0.0;

                foreach (var item in allProducts)
                {
                    totalPrice += Double.Round((item.Quantity * item.BookPublisher.Price), 2);
                }

             

                return loggedInUser?.UserCart;

            }

            return null;
        }
        public Double TotalPrice(string userId)
        {
            var totalPrice = 0.0;
            if (userId != null && !userId.IsNullOrEmpty())
            {
                var loggedInUser = _userRepository.Get(userId);

                var allProducts = loggedInUser?.UserCart?.BookInCart?.ToList();

                foreach (var item in allProducts)
                {
                    totalPrice += Double.Round((item.Quantity * item.BookPublisher.Price), 2);
                }

            }
            return totalPrice;
        }

        public bool orderProducts(string userId)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    OwnerId = userId,
                    Owner = loggedInUser
                };

                _orderRepository.Insert(order);

                List<BookPublisherInOrder> productInOrder = new List<BookPublisherInOrder>();

                var lista = userShoppingCart.BookInCart.Select(
                    x => new BookPublisherInOrder
                    {
                        Id = Guid.NewGuid(),
                        OrderedBookId = x.BookPublisher.Id,
                        OrderedBook = x.BookPublisher,
                        OrderId = order.Id,
                        Order = order,
                        Quantity = x.Quantity
                    }
                    ).ToList();

                productInOrder.AddRange(lista);

                foreach (var product in productInOrder)
                {
                    _productInOrderRepository.Insert(product);
                }

                loggedInUser.UserCart.BookInCart.Clear();
                _userRepository.Update(loggedInUser);
                return true;
            }
            return false;
        }
    }
}
