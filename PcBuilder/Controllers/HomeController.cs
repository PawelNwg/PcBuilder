using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PcBuilder.Data;
using PcBuilder.Interfaces;
using PcBuilder.Models;
using PcBuilder.Repositories;
using PcBuilder.Services.ImageToBlobStorage;
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
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IImageService imageService;

        public HomeController(ILogger<HomeController> logger, IRepositoryWrapper repositoryWrapper, IImageService imageService)
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
            this.imageService = imageService;
        }

        public IActionResult Index()
        {
            return View();          
        }

        [HttpPost]
        public async Task<IActionResult> Test(ProductFile productFile)
        {
            var path = await imageService.SaveImage(productFile.file);
            return View();
        }

        [HttpGet]
        public IActionResult Test()
        {
            return View();
        }

        public IActionResult PCInformations()
        {
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

        [HttpPost]
        public IActionResult testtt(string sortValue)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}