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

        public async Task<IActionResult> Index()
        {
            var products = await _repositoryWrapper.RepositoryProduct.GetAll();

            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Test(testcostam test)
        {
            var path = await imageService.SaveImage(test.file);
            return View();
        }

        [HttpGet]
        public IActionResult Test()
        {
            ViewBag.photo = imageService.GetImage(@"D:\STUDIA\SEMESTR VI\PracaInzynierska\PcBuilder\PcBuilder\wwwroot\BlobStorage\6f80b528-fa34-4cf6-9e32-671bbe98e8f7.jpg");
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