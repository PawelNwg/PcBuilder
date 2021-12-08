using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PcBuilder.Interfaces;
using PcBuilder.Models;
using PcBuilder.Services.Cart;
using PcBuilder.Services.Configurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace PcBuilder.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private CartManager _cartManager;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CartController(IRepositoryWrapper repositoryWrapper, IHttpContextAccessor httpContextAccessor)
        {
            _repositoryWrapper = repositoryWrapper;
            _cartManager = new CartManager(_repositoryWrapper, httpContextAccessor);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            await _cartManager.AddToCart(id);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int id)
        {
            _cartManager.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> CreateOrder(ConfiguratorViewModel model)
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier).Value;
            Order order = new Order()
            {
                UserId = userId,
                CreationDate = DateTime.Now,
                Status = "Nowe",
            };
            _repositoryWrapper.RepositoryOrder.Add(order);
            await _repositoryWrapper.RepositoryOrder.SaveOrder();
            return ViewComponent("Payment");
        }
    }
}