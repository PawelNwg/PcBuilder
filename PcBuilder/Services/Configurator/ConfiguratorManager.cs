using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PcBuilder.Interfaces;
using PcBuilder.Models;
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

        public ConfiguratorManager(IRepositoryWrapper repositoryWrapper, IHttpContextAccessor httpContext)
        {
            _repositoryWrapper = repositoryWrapper;
            _httpContext = httpContext;
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
            productToAdd.File = ""; // automapper here

            if (_httpContext.HttpContext.Request.Cookies.ContainsKey(Consts.Const.ConfiguratorSessionKey))
            {
                var cookie = GetConfiguration();
                if (cookie.Any(p => p.product.ProductId == productId))
                {
                    throw new Exception(); // add message that only one item can be added
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

        public async Task RemoveFromCart(int id)
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
                product = productToAdd,
                category = _repositoryWrapper.RepositoryCategory.GetById(productToAdd.Subcategory.CategoryId).Result,
                sum = productToAdd.Price
            };
            return newCartPosition;
        }

        private decimal GetCartTotalPrice()
        {
            var configuration = GetConfiguration();
            return configuration.Sum(k => k.product.Price);
        }

        public ConfiguratorViewModel PrepareViewModel()
        {
            return new ConfiguratorViewModel() { CartPositions = GetConfiguration(), TotalPrice = GetCartTotalPrice() };
        }
    }
}