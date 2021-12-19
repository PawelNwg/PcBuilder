using PcBuilder.Models;
using PcBuilder.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Services.Configurator
{
    public class ConfiguratorPosition
    {
        public ConfigurationProductViewModel product { get; set; }
        public Category category { get; set; }

        public int quantity { get; set; }
        public decimal sum { get; set; }
    }
}