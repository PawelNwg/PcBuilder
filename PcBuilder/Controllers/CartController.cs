using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PcBuilder.Interfaces;
using PcBuilder.Services.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}