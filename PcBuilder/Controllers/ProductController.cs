﻿using Microsoft.AspNetCore.Mvc;
using PcBuilder.Interfaces;
using PcBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ProductController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SelectedProductList(int id)
        {
            var subcategory = await _repositoryWrapper.RepositorySubcategory.GetByCondition(a => a.CategoryId == id);

            var selectedProducts = await _repositoryWrapper.RepositoryProduct.GetByCondition(p => p.SubCategoryId == subcategory[0].SubcategoryId);

            return View(selectedProducts);
        }
    }
}