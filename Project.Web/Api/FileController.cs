using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Project.Domain.Context;
using Project.Domain.Products;
using Project.Domain.Tools;
using Project.Web.Models.Images;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.Api
{
    [Route("api/file")]
    [ApiController]
    public class FileController : BaseApiController
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IFileManager _fileManager;

        public FileController(IDataBaseContext context, IMapper mapper, IFileManager fileManager)
        {
            _context = context;
            _mapper = mapper;
            _fileManager = fileManager;
        }

        [HttpGet, Route("get-background"), AllowAnonymous]
        public async Task<ActionResult> GetBackground()
        {
            Image background = _context.Images.FirstOrDefault(i => i.BackGround);

            background.Base64 = await _fileManager.ReadFile(background.Path);

            return Ok(_mapper.Map<ImageModel>(background));
        }


        [HttpPost, Route("upload-image")]
        public async Task<ActionResult> UploadImage(Image image)
        {
            if (image.BackGround)
            {
                foreach (var backgroundImage in _context.Images.Where(i => i.BackGround))
                {
                    backgroundImage.BackGround = false;
                }

                var backgroundProduct = _context.Products.FirstOrDefault(p => p.Name == "Background");
                if (backgroundProduct == null)
                    backgroundProduct = new Product { Active = false, Name = "Background" };


                var backgroundCategory = _context.Categories.FirstOrDefault(c => c.Name == "Background");
                if (backgroundCategory == null)
                    backgroundCategory = new Category { Name = "Background" };

                backgroundProduct.Category = backgroundCategory;
                image.Product = backgroundProduct;
            }

            await _fileManager.SaveImage(image, _context);

            return Ok();
        }

        [HttpDelete, Route("delete-image/{id}")]
        public async Task<ActionResult> DeleteImage(int id)
        {
            var image = _context.Images.Find(id);

            await _fileManager.DeleteFile(image.Path);

            _context.Images.Remove(image);
            _context.SaveChanges();

            return Ok();
        }
    }
}
