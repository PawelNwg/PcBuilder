using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcBuilder.Interfaces;
using PcBuilder.Services.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Controllers
{
    public class CartController : Controller
    {
        private CartManager cartManager;
        private ISessionManager sessionManager { get; set; }

        private readonly IRepositoryWrapper _repositoryWrapper;

        public CartController(IRepositoryWrapper repositoryWrapper, IHttpContextAccessor httpContextAccessor)
        {
            _repositoryWrapper = repositoryWrapper;
            sessionManager = new SessionManager(httpContextAccessor);
            cartManager = new CartManager(sessionManager, _repositoryWrapper);
        }

        public IActionResult Index()
        {
            var cartPosition = cartManager.GetCart();
            var totalPrice = cartManager.GetCartSum();
            CartViewModel cartViewModel = new CartViewModel { CartPositions = cartPosition, TotalPrice = totalPrice };
            return View(cartViewModel);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            await cartManager.AddToCart(id);
            return RedirectToAction("Index");
        }

    }
}