using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PcBuilder.Interfaces;
using PcBuilder.Models;
using PcBuilder.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Services.Configurator
{
    public class ConfiguratorManager
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;

        public ConfiguratorManager(IRepositoryWrapper repositoryWrapper, IHttpContextAccessor httpContext, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _httpContext = httpContext;
            _mapper = mapper;
        }

        public List<ConfiguratorPosition> GetConfiguration()
        {
            if (Get(Consts.Const.ConfiguratorSessionKey) != null)
            {
                return JsonConvert.DeserializeObject<List<ConfiguratorPosition>>(Get(Consts.Const.ConfiguratorSessionKey));
            }
            return null;
        }

        public async Task AddToConfiguration(int productId)
        {
            var productToAdd = await _repositoryWrapper.RepositoryProduct.GetById(productId);
            if (productToAdd == null)
            {
                throw new Exception();
            }

            if (_httpContext.HttpContext.Request.Cookies.ContainsKey(Consts.Const.ConfiguratorSessionKey))
            {
                var cookie = GetConfiguration();
                if (cookie.Any(p => p.product.ProductId == productId))
                {
                    var editedProduct = cookie.Find(p => p.product.ProductId == productId);
                    editedProduct.quantity++;
                    editedProduct.sum = GetOnePositionSum(editedProduct);
                }
                else
                {
                    cookie.Add(PrepareNewProductToConfiguration(productToAdd));
                }
                Remove(Consts.Const.ConfiguratorSessionKey);
                AppendCookie(cookie);
            }
            else
            {
                List<ConfiguratorPosition> configuratorPositions = new List<ConfiguratorPosition>() { PrepareNewProductToConfiguration(productToAdd) };
                AppendCookie(configuratorPositions);
            }
        }

        private decimal GetOnePositionSum(ConfiguratorPosition configuratorPosition)
        {
            return (configuratorPosition.quantity * configuratorPosition.product.Price);
        }

        public async Task RemoveFromConfigurator(int id)
        {
            var configurator = GetConfiguration();
            var configuratorPosition = configurator.Find(x => x.product.ProductId == id);
            if (configuratorPosition != null)
            {
                configurator.Remove(configuratorPosition);
            }
            Remove(Consts.Const.ConfiguratorSessionKey);
            AppendCookie(configurator);
        }

        private string Get(string key)
        {
            return _httpContext.HttpContext.Request.Cookies[key];
        }

        private void Remove(string key)
        {
            _httpContext.HttpContext.Response.Cookies.Delete(key);
        }

        private void AppendCookie(List<ConfiguratorPosition> configurationPosition)
        {
            _httpContext.HttpContext.Response.Cookies.Append(
                Consts.Const.ConfiguratorSessionKey,
                JsonConvert.SerializeObject(configurationPosition)
                );
        }

        private ConfiguratorPosition PrepareNewProductToConfiguration(Product productToAdd)
        {
            var newCartPosition = new ConfiguratorPosition()
            {
                product = _mapper.Map<ConfigurationProductViewModel>(productToAdd),
                category = _repositoryWrapper.RepositoryCategory.GetOneByCodition(c => c.Subcategories.Any(s => s.SubcategoryId == productToAdd.SubCategoryId)).GetAwaiter().GetResult(),
                quantity = 1,
                sum = productToAdd.Price
            };

            return newCartPosition;
        }

        private decimal GetCartTotalPrice()
        {
            var configuration = GetConfiguration();
            if (configuration != null)
            {
                return configuration.Sum(k => (k.quantity * k.product.Price));
            }
            return 0;
        }

        public ConfiguratorViewModel PrepareViewModel()
        {
            return new ConfiguratorViewModel() { ConfiguratorPositions = GetConfiguration(), TotalPrice = GetCartTotalPrice()};
        }
    }
}