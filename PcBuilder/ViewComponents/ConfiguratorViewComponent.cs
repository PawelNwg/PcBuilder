using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcBuilder.Interfaces;
using PcBuilder.Services.Configurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.ViewComponents
{
    public class ConfiguratorViewComponent : ViewComponent
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private ConfiguratorManager _configuratorManager;

        public ConfiguratorViewComponent(IRepositoryWrapper repositoryWrapper, IHttpContextAccessor httpContextAccessor)
        {
            _configuratorManager = new ConfiguratorManager(_repositoryWrapper, httpContextAccessor);
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("Configurator", _configuratorManager.PrepareViewModel()));
        }
    }
}