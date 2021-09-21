using Microsoft.AspNetCore.Mvc;
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
        private ISessionManager _session;

        public CartManager(ISessionManager session, IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _session = session;
        }

        public List<CartPosition> GetCart()
        {
            List<CartPosition> cart;
            if (_session.Get(Consts.Const.CartSessionKey) == null)
            {
                cart = new List<CartPosition>();
            }
            else
            {
                cart = Deserialize<List<CartPosition>>(_session.Get(Consts.Const.CartSessionKey));
            }
            return cart;
        }

        public async Task AddToCart(int productId)
        {
            var cart = GetCart();
            var cartPosition = cart.Find(p => p.product.ProductId == productId);
            if (cartPosition != null)
                cartPosition.quantity++;
            else
            {
                var productToAdd = await _repositoryWrapper.RepositoryProduct.GetById(productId);
                if (productToAdd != null)
                {
                    var newCartPosition = new CartPosition()
                    {
                        product = productToAdd,
                        quantity = 1,
                        sum = productToAdd.Price
                    };
                    cart.Add(newCartPosition);
                }
            }
            _session.Set(Consts.Const.CartSessionKey, Serialize(cart));
        }

        private T Deserialize<T>(byte[] param)
        {
            using (MemoryStream ms = new MemoryStream(param))
            {
                IFormatter br = new BinaryFormatter();
                return (T)br.Deserialize(ms);
            }
        }

        private byte[] Serialize<T>(T param)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Position = 0;
                IFormatter br = new BinaryFormatter();
                br.Serialize(ms, param);
                return ms.ToArray();
            }
        }

        public int RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var cartPosition = cart.Find(x => x.product.ProductId == productId);
            if (cartPosition != null)
            {
                if (cartPosition.quantity > 1)
                {
                    cartPosition.quantity--;
                    return cartPosition.quantity;
                }
                else
                {
                    cart.Remove(cartPosition);
                }
            }
            return 0;
        }

        public decimal GetCartSum()
        {
            var cart = GetCart();
            return cart.Sum(k => (k.quantity * k.product.Price));
        }

        public int GetCartItemsNumber()
        {
            var cart = GetCart();
            return cart.Sum(k => k.quantity);
        }

        //public Order CreateOrder(Order order, string userId)
        //{
        //var cart = GetCart();
        //order.CreationDate = DateTime.Now;
        ////order.AppUser = (int)userId;
        //order.Status = "created";

        ////_repositoryWrapper.RepositoryProduct.Create(order); make order repository wrapper

        //foreach (var cartElement in cart)
        //{
        //    var cartPosition = new CartPosition()
        //    {
        //        sum = cartElement.product.Price,
        //        quantity = cartElement.quantity,
        //        product.ProductId = cartElement.product.ProductId
        //    };
        //}
        //return 0;
        //}

        //public void GetEmptyCart()
        //{
        //    _session.Set<List<CartPosition>>(Consts.Const.CartSessionKey, Serialize(null));
        //}
    }
}