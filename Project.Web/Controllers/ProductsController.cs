﻿using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }      
        
        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult ShoppingList()
        {
            return View();
        }
    }
}
