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
    public class CartViewComponent : ViewComponent
    {
        private CartManager _cartManager;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CartViewComponent(IRepositoryWrapper repositoryWrapper, IHttpContextAccessor httpContextAccessor)
        {
            _cartManager = new CartManager(_repositoryWrapper, httpContextAccessor);
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("Cart", _cartManager.PrepareViewModel()));
        }
    }
}