using Microsoft.AspNetCore.Mvc;
using PcBuilder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.ViewComponents
{
    public class CarouselViewComponent : ViewComponent
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CarouselViewComponent(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _repositoryWrapper.RepositoryProduct.GetAll();
            return await Task.FromResult((IViewComponentResult)View("Carousel", products));
        }
    }
}
