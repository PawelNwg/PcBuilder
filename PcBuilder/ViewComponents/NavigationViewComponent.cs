using Microsoft.AspNetCore.Mvc;
using PcBuilder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.ViewComponents
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        private NavigationViewComponent(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _repositoryWrapper.RepositoryCategory.GetAll();
            return await Task.FromResult((IViewComponentResult)View("Navigation", categories));
        }
    }
}