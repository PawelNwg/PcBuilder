using PcBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Services.Configurator
{
    public class ConfiguratorCheckViewModel
    {
        public List<Category> Categories { get; set; }

        public List<Category> FilledCategories { get; set; }
    }
}