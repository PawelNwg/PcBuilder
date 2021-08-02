using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PcBuilder.Models;
using PcBuilder.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RepositoryWrapper _repositoryWrapper;

        public HomeController(ILogger<HomeController> logger, RepositoryWrapper repositoryWrapper)
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

        public IActionResult Index()
        {
            _repositoryWrapper.BeginTransaction();
            var kategorie = _repositoryWrapper.RepositoryCategory.GetAll();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}