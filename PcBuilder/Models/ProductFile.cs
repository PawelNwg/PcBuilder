using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Models
{
    public class ProductFile
    {
        public IFormFile file { get; set; }
    }
}