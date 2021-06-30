using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Project.Domain.Context;
using Project.Domain.Products;
using Project.Domain.Shopping;
using Project.Web.Models.Shopping;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.Api
{
    [Route("api/[controller]")]
    public class ShoppingController : BaseApiController
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public ShoppingController(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet, Route("get-shopping-list/{id}")]
        public async Task<ActionResult> GetShoppingList(int id)
        {
            var user = _context.Users.Find(id);

            var list = _context.ShoppingLists.FirstOrDefault(sp => sp.UserId == user.Id);
            var listModel = _mapper.Map<ShoppingListModel>(list);

            await Task.CompletedTask;

            return Ok(listModel);
        }

        [HttpPost, Route("{userId}")]
        public async Task<ActionResult> SaveShoppingList(int userId, CartProduct[] cart)
        {
            ShoppingList shoppingList = _context.ShoppingLists.FirstOrDefault(s => s.UserId == userId);

            bool exist = true;

            if (shoppingList == null)
            {
                exist = false;
                shoppingList = new();
            }
            if (exist)
                _context.ShoppingLists.Update(shoppingList);
            else
                _context.ShoppingLists.Add(shoppingList);

            shoppingList.Items = null;
            shoppingList.User = _context.Users.Find(userId);
            shoppingList.UserId = shoppingList.User.Id;
            _context.SaveChanges();

            foreach (CartProduct product in cart)
            {
                Item item = new()
                {
                    Amount = product.Amount,
                    ProductId = product.Id,
                    ShoppingListId = shoppingList.Id,
                    ShoppingList = shoppingList
                };

                _context.Items.Add(item);
                _context.SaveChanges();

                shoppingList.Items.Add(item);
            }


            _context.ShoppingLists.Update(shoppingList);

            _context.SaveChanges();

            await Task.CompletedTask;

            return Ok();
        }
    }
}
