using PcBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Services.Configurator
{
    public class ConfiguratorPosition
    {
        public Product product { get; set; }
        public Category category { get; set; }
        public decimal sum { get; set; }
    }
}