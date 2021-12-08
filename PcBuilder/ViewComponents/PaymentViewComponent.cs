using Microsoft.AspNetCore.Mvc;
using PcBuilder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.ViewComponents
{
    public class PaymentViewComponent : ViewComponent
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public PaymentViewComponent(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("Payment"));
        }
    }
}
