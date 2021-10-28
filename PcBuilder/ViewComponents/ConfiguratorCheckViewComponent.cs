using Microsoft.AspNetCore.Mvc;
using PcBuilder.Interfaces;
using PcBuilder.Models;
using PcBuilder.Services.Configurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.ViewComponents
{
    public class ConfiguratorCheckViewComponent : ViewComponent
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private ConfiguratorManager _configuratorManager;

        public ConfiguratorCheckViewComponent(IRepositoryWrapper repositoryWrapper, ConfiguratorManager configuratorManager)
        {
            _repositoryWrapper = repositoryWrapper;
            _configuratorManager = configuratorManager;
        }

        private List<Category> GetFilledCategories()
        {
            List<Category> filledCategories = new List<Category>();
            var configuration = _configuratorManager.GetConfiguration();
            if (configuration == null) return new List<Category>();
            foreach (var conf in configuration)
            {
                if (!filledCategories.Any(x => x.CategoryId == conf.category.CategoryId))
                    filledCategories.Add(conf.category);
            }
            return filledCategories.Distinct().ToList();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _repositoryWrapper.RepositoryCategory.GetAll();
            ConfiguratorCheckViewModel configurationCheckViewModel = new ConfiguratorCheckViewModel() { Categories = categories, FilledCategories = GetFilledCategories() };
            return await Task.FromResult((IViewComponentResult)View("ConfiguratorCheck", configurationCheckViewModel));
        }
    }
}