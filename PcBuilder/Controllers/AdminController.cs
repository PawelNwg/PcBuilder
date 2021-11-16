using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PcBuilder.Interfaces;
using PcBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(IRepositoryWrapper repositoryWrapper, UserManager<AppUser> userManager)
        {
            _repositoryWrapper = repositoryWrapper;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UserList()
        {
            var users = await _repositoryWrapper.RepositoryUser.GetAll();
            return View(users);
        }
        public async Task<IActionResult> BlockUser(string id, DateTime? endDate)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) return Unauthorized();

            if (endDate == null)
                endDate = DateTime.Now.AddYears(100);

            await _userManager.SetLockoutEnabledAsync(user, true).ConfigureAwait(false);

            await _userManager.SetLockoutEndDateAsync(user, endDate);

            return RedirectToAction("UserList", "Admin");
        }

        public async Task<IActionResult> UnblockUser(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) return Unauthorized();

             await _userManager.SetLockoutEnabledAsync(user, false);

             await _userManager.SetLockoutEndDateAsync(user, null);

            return RedirectToAction("UserList", "Admin");
        }
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _repositoryWrapper.RepositoryProduct.GetById(id);

            if (product == null) return NotFound();

            _repositoryWrapper.RepositoryProduct.DeleteProduct(product);
            await _repositoryWrapper.RepositoryProduct.SaveProduct();

            return RedirectToAction("Index", "Home");
        }
    }
}
