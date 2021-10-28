using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcBuilder.Interfaces;
using PcBuilder.Services.Configurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Controllers
{
    [Authorize]
    public class ConfiguratorController : Controller
    {
        private ConfiguratorManager _configuratorManager;

        private readonly IRepositoryWrapper _repositoryWrapper;

        public ConfiguratorController(IRepositoryWrapper repositoryWrapper, IHttpContextAccessor httpContextAccessor)
        {
            _repositoryWrapper = repositoryWrapper;
            _configuratorManager = new ConfiguratorManager(_repositoryWrapper, httpContextAccessor);
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _repositoryWrapper.RepositoryCategory.GetAll();
            return View(categories);
        }

        public async Task<IActionResult> AddToConfigurator(int id)
        {
            await _configuratorManager.AddToConfiguration(id);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromConfigurator(int id)
        {
            _configuratorManager.RemoveFromConfigurator(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SelectedConfiguratorCategory(int id)
        {
            var subcategory = await _repositoryWrapper.RepositorySubcategory.GetByCondition(a => a.CategoryId == id);

            var selectedProducts = await _repositoryWrapper.RepositoryProduct.GetByCondition(p => p.SubCategoryId == subcategory[0].SubcategoryId);

            return View(selectedProducts);
        }

        public async Task<IActionResult> ValidateConfiguration()
        {
            return View();
        }
    }
}