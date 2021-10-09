using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcBuilder.Interfaces;
using PcBuilder.Models;
using PcBuilder.Models.ViewModels;
using PcBuilder.Services.ImageToBlobStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IImageService _imageService;

        public ProductController(IRepositoryWrapper repositoryWrapper, IImageService imageService)
        {
            _repositoryWrapper = repositoryWrapper;
            _imageService = imageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SelectedProductList(int id)
        {
            var subcategory = await _repositoryWrapper.RepositorySubcategory.GetByCondition(a => a.CategoryId == id);

            var selectedProducts = await _repositoryWrapper.RepositoryProduct.GetByCondition(p => p.SubCategoryId == subcategory[0].SubcategoryId); // change

            return View(selectedProducts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var selectedProduct = await _repositoryWrapper.RepositoryProduct.GetById(id);
            return View(selectedProduct);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            var categories = _repositoryWrapper.RepositoryCategory.GetAll().Result;
            AddProductViewModel addProductViewModel = new AddProductViewModel { Categories = categories };
            return View(addProductViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(AddProductViewModel product)
        {
            if (ModelState.IsValid && product != null)
            {
                Subcategory subcategory = await _repositoryWrapper.RepositorySubcategory.GetOneByCodition(s => s.CategoryId == product.CategoryId);
                Product productToAdd = new Product()
                {
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    SubCategoryId = subcategory.SubcategoryId,
                    File = null,
                };
                _repositoryWrapper.RepositoryProduct.Create(productToAdd);
                return RedirectToAction("AddImageToProduct", productToAdd.ProductId);
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> AddImageToProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddImageToProduct(ProductFile productFile)
        {
            var path = await _imageService.SaveImage(productFile.file);
            //if (productId != 0)
            //    _repositoryWrapper.RepositoryProduct.GetById();
            return View();
        }
    }
}