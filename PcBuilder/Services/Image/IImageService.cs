using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.Services.ImageToBlobStorage
{
    public interface IImageService
    {
        Task<string> SaveImage(IFormFile file);

        string GetImage(string path);
    }
}