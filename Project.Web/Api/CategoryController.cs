using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Project.Domain.Context;
using Project.Domain.Products;
using Project.Web.Models.Product;

using Starlight.Core.DbHelper;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseApiController
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public CategoryController(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(List<CategoryModel>), 200)]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var categories = _context.Categories.ToList();

            List<CategoryModel> models = new();

            foreach (var category in categories)
            {
                if (category.Name == "Background")
                    continue;

                models.Add(_mapper.Map<CategoryModel>(category));
            }

            await Task.CompletedTask;

            return Ok(models);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCategory(CategoryModel categoryModel)
        {
            var exist = _context.Categories.Any(c => c.Name == categoryModel.Name);

            if (exist)
                return BadRequest();

            var category = _mapper.Map<Category>(categoryModel);

            category.Products = new List<Product>();
            _context.Add(category);
            _context.SaveChanges();

            await Task.CompletedTask;

            return Ok();
        }
    }
}
