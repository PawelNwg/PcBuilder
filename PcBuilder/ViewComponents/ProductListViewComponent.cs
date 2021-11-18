using Microsoft.AspNetCore.Mvc;
using PcBuilder.Interfaces;
using PcBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.ViewComponents
{
    public class ProductListViewComponent : ViewComponent
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ProductListViewComponent(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<IViewComponentResult> InvokeAsync(string sortOrder = null, string searchString = null, int categoryId = 0, int subcategoryId = 0)
        {
            List<Product> products = await _repositoryWrapper.RepositoryProduct.GetAll();

            if (categoryId is not 0)
            {
                var subcategory = await _repositoryWrapper.RepositorySubcategory.GetOneByCodition(a => a.CategoryId == categoryId);
                products = products.Where(p => p.SubCategoryId == subcategory.SubcategoryId).ToList();
            }

            if (subcategoryId is not 0)
            {              
                products = products.Where(p => p.SubCategoryId == subcategoryId).ToList();
            }

            if (!String.IsNullOrEmpty(searchString))
                products = SearchProduct(searchString, products);

            if (!String.IsNullOrEmpty(sortOrder))
                products = SortProducts(sortOrder, products);

            return await Task.FromResult((IViewComponentResult)View("ProductList", products));
        }

        private List<Product> SortProducts(string sortOrder, List<Product> products)
        {

            if (!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "nameAsc":
                    {
                        return products.OrderBy(p => p.Name).ToList();
                    }
                    case "nameDesc":
                    {
                        return products.OrderByDescending(p => p.Name).ToList();
                    }
                    case "priceAsc":
                    {
                        return products.OrderBy(p => p.Price).ToList();
                    }
                    case "priceDesc":
                    {
                        return products.OrderByDescending(p => p.Price).ToList();
                    }
                    default:
                        break;
                }
            }
            return products;
        }

        private List<Product> SearchProduct(string searchString, List<Product> products)
        {
                return products.Where(p => p.Name.ToLower().Contains(searchString.ToLower())).ToList();            
        }
    }
}


