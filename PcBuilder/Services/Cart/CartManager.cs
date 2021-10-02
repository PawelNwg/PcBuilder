using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PcBuilder.Interfaces;
using PcBuilder.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace PcBuilder.Services.Cart
{
    public class CartManager
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IHttpContextAccessor _httpContext;

        public CartManager(IRepositoryWrapper repositoryWrapper, IHttpContextAccessor httpContext)
        {
            _repositoryWrapper = repositoryWrapper;
            _httpContext = httpContext;
        }

        public List<CartPosition> GetCart()
        {
            if (Get(Consts.Const.CartSessionKey) != null)
            {
                return JsonConvert.DeserializeObject<List<CartPosition>>(Get(Consts.Const.CartSessionKey));
            }
            return null;
        }

        public async Task AddToCart(int productId)
        {
            var productToAdd = await _repositoryWrapper.RepositoryProduct.GetById(productId);
            if (productToAdd == null)
            {
                throw new Exception();
            }
            productToAdd.File = ""; // automapper here

            if (_httpContext.HttpContext.Request.Cookies.ContainsKey(Consts.Const.CartSessionKey))
            {
                var cookie = GetCart();
                if (cookie.Any(p => p.product.ProductId == productId))
                {
                    var editedProduct = cookie.Find(p => p.product.ProductId == productId);
                    editedProduct.quantity++;
                    editedProduct.sum = GetOnePositionSum(editedProduct);
                }
                else
                {
                    cookie.Add(PrepareNewProductToCart(productToAdd));
                }
                Remove(Consts.Const.CartSessionKey);
                AppendCookie(cookie);
            }
            else
            {
                List<CartPosition> cartPositions = new List<CartPosition>() { PrepareNewProductToCart(productToAdd) };
                AppendCookie(cartPositions);
            }
        }

        public async Task RemoveFromCart(int id)
        {
            var cart = GetCart();
            var cartPosition = cart.Find(x => x.product.ProductId == id);
            if (cartPosition != null)
            {
                if (cartPosition.quantity > 1)
                {
                    cartPosition.quantity--;
                    cartPosition.sum = GetOnePositionSum(cartPosition);
                }
                else
                {
                    cart.Remove(cartPosition);
                }
            }
            Remove(Consts.Const.CartSessionKey);
            AppendCookie(cart);
        }

        public CartViewModel PrepareViewModel()
        {
            return new CartViewModel() { CartPositions = GetCart(), TotalPrice = GetCartTotalPrice() };
        }

        private decimal GetOnePositionSum(CartPosition cartPosition)
        {
            return (cartPosition.quantity * cartPosition.product.Price);
        }

        private decimal GetCartTotalPrice()
        {
            var cart = GetCart();
            return cart.Sum(k => (k.quantity * k.product.Price));
        }

        private string Get(string key)
        {
            return _httpContext.HttpContext.Request.Cookies[key];
        }

        private void Remove(string key)
        {
            _httpContext.HttpContext.Response.Cookies.Delete(key);
        }

        private void AppendCookie(List<CartPosition> cartPositions)
        {
            _httpContext.HttpContext.Response.Cookies.Append(
                Consts.Const.CartSessionKey,
                JsonConvert.SerializeObject(cartPositions)
                );
        }

        private CartPosition PrepareNewProductToCart(Product productToAdd)
        {
            var newCartPosition = new CartPosition()
            {
                product = productToAdd,
                quantity = 1,
                sum = productToAdd.Price
            };
            return newCartPosition;
        }
    }
}