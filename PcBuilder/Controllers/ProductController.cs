using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcBuilder.Data;
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
        private readonly ApplicationDbContext _context;

        public ProductController(IRepositoryWrapper repositoryWrapper, IImageService imageService, ApplicationDbContext context)
        {
            _repositoryWrapper = repositoryWrapper;
            _imageService = imageService;
            _context = context;
        }

        public IActionResult Index(int id)
        {
            return View(id);
        }

        public IActionResult SelectedProductList(string searchString = null, string sortOrder = null, int id = 0, int subcategoryId = 0)
        {
            var categoryId = id;
            return ViewComponent("ProductList", new { searchString, sortOrder, categoryId, subcategoryId });
        }

        public async Task<IActionResult> Details(int id)
        {
            var selectedProduct = await _repositoryWrapper.RepositoryProduct.GetById(id);
            return View(selectedProduct);
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddProduct()
        {
            var categories = _repositoryWrapper.RepositoryCategory.GetAll().Result;
            AddProductViewModel addProductViewModel = new AddProductViewModel { Categories = categories };
            return View(addProductViewModel);
        }

        [HttpPost]
        [Authorize]
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
                    Quantity = 1,
                };
                _repositoryWrapper.RepositoryProduct.Add(productToAdd);
                await _repositoryWrapper.RepositoryProduct.SaveProduct();
                TempData["ProductToUpload"] = productToAdd.ProductId;
                return RedirectToAction("AddImageToProduct", new { productId = productToAdd.ProductId });
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> AddImageToProduct(string productId = "")
        {
            //var productIdToAddImage = _repositoryWrapper.RepositoryProduct.GetById(Int32.Parse(productId)).Result;
            ProductFile productFile = new ProductFile() { productID = Int32.Parse(productId) };
            return View(productFile);
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddImageToProduct(ProductFile productFile)
        {
            var path = await _imageService.SaveImage(productFile.file);
            if (productFile.productID != 0)
            {
                var updatedProduct = _repositoryWrapper.RepositoryProduct.GetById(productFile.productID).Result;
                updatedProduct.File = path;
                _context.Products.Update(updatedProduct);
            }
            else
            {
                var updatedProduct = _repositoryWrapper.RepositoryProduct.GetById(productFile.productID).Result;
                _context.Products.Remove(updatedProduct);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}