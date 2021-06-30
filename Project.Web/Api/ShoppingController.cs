using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Project.Domain.Context;
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

            return Ok();
        }
    }
}
