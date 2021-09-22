using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcBuilder.Interfaces;
using PcBuilder.Services.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.ViewComponents
{
    public class CartCountViewComponent : ViewComponent
    {
        private CartManager cartManager;
        private ISessionManager sessionManager { get; set; }

        private readonly IRepositoryWrapper _repositoryWrapper;

        public CartCountViewComponent(IRepositoryWrapper repositoryWrapper, IHttpContextAccessor httpContextAccessor)
        {
            sessionManager = new SessionManager(httpContextAccessor);
            cartManager = new CartManager(sessionManager, _repositoryWrapper);
        }

        public int GetCartSize()
        {
            return cartManager.GetCartItemsNumber();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewData["CartSize"] = GetCartSize();
            return await Task.FromResult((IViewComponentResult)View("CartCount"));
        }
    }
}