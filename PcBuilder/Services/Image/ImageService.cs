using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;

namespace PcBuilder.Services.ImageToBlobStorage
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment env;

        public ImageService(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public string GetImage(string path)
        {
            if (path != null)
            {
                string blobStorage = Path.Combine(env.WebRootPath, "BlobStorage");
                string filePath = Path.Combine(blobStorage, path);
                return Convert.ToBase64String(File.ReadAllBytes(filePath));
            }
            return string.Empty;
        }

        public async Task<string> SaveImage(IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName);
            string fileName = Guid.NewGuid() + extension;
            IImageEncoder encoder = extension switch
            {
                ".jpeg" => new JpegEncoder(),
                ".bmp" => new BmpEncoder(),
                ".gif" => new GifEncoder(),
                _ => new PngEncoder()
            };
            string path = Path.Combine(env.WebRootPath, "BlobStorage");
            string filePath = Path.Combine(path, fileName);
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                using (var image = Image.Load(file.OpenReadStream()))
                {
                    image.Mutate(x => x.Resize(300, 300));
                    //await image.SaveAsync(filePath, encoder);
                    //await stream.WriteAsync(image.Sa);
                    await image.SaveAsync(stream, encoder);
                }
            }
            return filePath;
        }
    }
}