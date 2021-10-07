﻿using PcBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Services.Configurator
{
    public class ConfiguratorViewModel
    {
        public List<ConfiguratorPosition> ConfiguratorPositions { get; set; }
        public decimal TotalPrice { get; set; }
    }
}