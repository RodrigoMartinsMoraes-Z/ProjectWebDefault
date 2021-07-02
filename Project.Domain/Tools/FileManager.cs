using Project.Domain.Context;
using Project.Domain.Products;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Tools
{
    public interface IFileManager
    {
        Task SaveImage(Image image, IDataBaseContext _context);
        Task<string> ReadFile(string path);
        Task DeleteFile(string path);
    }

    public class FileManager : IFileManager
    {
        public async Task SaveImage(Image image, IDataBaseContext _context)
        {
            if (image.ProductId == 0)
                throw new Exception("Image must contain productId");

            if (image.Id > 0)
                return;

            int start = image.Base64.IndexOf("data:") + "data:".Length;
            int end = image.Base64.IndexOf(";", start);
            image.Type = image.Base64[start..end];

            string base64 = image.Base64[(image.Base64.LastIndexOf(',') + 1)..].Trim();
            _context.Images.Add(image);
            _context.SaveChanges();

            image.Path = await SaveFileAsBase64(base64, image.Id);
            _context.SaveChanges();
        }

        private static async Task<string> SaveFileAsBase64(string base64, int id)
        {
            //Convert the Byte Array to Base64 Encoded string.
            string base64String = base64;

            //Convert Base64 Encoded string to Byte Array.
            byte[] imageBytes = Convert.FromBase64String(base64String);

            //Save the Byte Array as File.
            System.IO.Directory.CreateDirectory("./Files");
            string filePath = "./Files/" + id;
            System.IO.File.WriteAllBytes(filePath, imageBytes);

            await Task.CompletedTask;

            return filePath;
        }

        public async Task<string> ReadFile(string path)
        {

            var bytes = System.IO.File.ReadAllBytes(path);

            var base64 = Convert.ToBase64String(bytes);

            await Task.CompletedTask;

            return base64;

        }

        public Task DeleteFile(string path)
        {
            System.IO.File.Delete(path);

            return Task.CompletedTask;
        }
    }
}
