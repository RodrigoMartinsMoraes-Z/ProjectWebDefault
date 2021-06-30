using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Project.Domain.Context;
using Project.Domain.Products;
using Project.Domain.Tools;
using Project.Web.Models.Product;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IFileManager _fileManager;

        public ProductController(IDataBaseContext context, IMapper mapper, IFileManager fileManager)
        {
            _context = context;
            _mapper = mapper;
            _fileManager = fileManager;
        }

        [HttpPut, Route("update")]
        public async Task<ActionResult> UpdateProduct(ProductModel model)
        {
            Product product = _mapper.Map<Product>(model);

            var exist = _context.Products.FirstOrDefault(p => p.Name == product.Name);

            if (exist == null)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }
            else if (product.Id == 0)
                product.Id = exist.Id;

            foreach (var image in product.Images)
            {
                if (image.Id > 0)
                    continue;

                image.ProductId = product.Id;
                await _fileManager.SaveImage(image, _context);
            }

            product.Images = null;
            if (exist != null)
            {
                exist.Name = product.Name;
                exist.Description = product.Description;
                exist.Price = product.Price;
                exist.Category = product.Category;
                exist.CategoryId = product.CategoryId;
                _context.Products.Update(exist);
            }

            _context.SaveChanges();

            return Ok();
        }

        [HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(ProductModel), 200)]
        public async Task<ActionResult> GetProduct(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
                return NotFound();

            foreach (var image in product.Images)
            {
                var base64 = await _fileManager.ReadFile(image.Path);

                image.Base64 = $"data:{image.Type};base64,{base64}";
            }

            await Task.CompletedTask;

            return Ok(_mapper.Map<ProductModel>(product));

        }

        [HttpGet, Route("list")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ProductModel[]), 200)]
        public async Task<ActionResult> ListOfProduct(int take = 10, int skip = 0)
        {
            var products = _context.Products.Where(p=>p.Category.Name != "Background").OrderBy(p => p.Id).Skip(skip).Take(take).ToList();

            IList<ProductModel> productsModel = new List<ProductModel>();

            foreach (var product in products)
            {
                foreach(var image in product.Images)
                {
                    image.Base64 = await _fileManager.ReadFile(image.Path);
                }

                productsModel.Add(_mapper.Map<ProductModel>(product));
            }

            await Task.CompletedTask;
            return Ok(productsModel);
        }
    }
}
