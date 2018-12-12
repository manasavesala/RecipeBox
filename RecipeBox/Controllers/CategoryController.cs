using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;

namespace RecipeBox.Controllers
{
    public class CategoryController : Controller
    {
        [HttpGet("/category/new")]
        public ActionResult New()
        {
            List<Category> allCategories = Category.GetAll();
            return View("New", allCategories);
        }
        [HttpPost("/addCategory")]
        public ActionResult Create(string CategoryName)
        {
            Category newCategory = new Category(CategoryName);
            newCategory.Save();
            return RedirectToAction("New");
        }
    }
}
