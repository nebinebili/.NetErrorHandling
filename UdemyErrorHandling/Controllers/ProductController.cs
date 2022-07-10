using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyErrorHandling.Filter;

namespace UdemyErrorHandling.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            throw new Exception("Error in Database");
            return View();
        }
        
    }
}
