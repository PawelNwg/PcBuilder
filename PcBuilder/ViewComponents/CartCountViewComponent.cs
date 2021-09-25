using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public CartCountViewComponent()
        {
        }

        public int GetCartSize()
        {
            if (Request.Cookies.ContainsKey(Consts.Const.CartSessionKey))
            {
                var cartPositions = JsonConvert.DeserializeObject<List<CartPosition>>(Request.Cookies[Consts.Const.CartSessionKey]);
                return cartPositions.Count;
            }
            return 0;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewData["CartSize"] = GetCartSize();
            return await Task.FromResult((IViewComponentResult)View("CartCount"));
        }
    }
}