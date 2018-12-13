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


        [HttpGet("/category/{categoryId}/recipes")]
        public ActionResult View(int categoryId)
        {
            Dictionary<string, object> model = new Dictionary<string, object> { };
            Category category = Category.Find(categoryId);
            List<Recipe> recipesByCategory = Category.FindRecipesOfCategory(categoryId);
            List<Recipe> allRecipes = Recipe.GetAll();
            model.Add("category", category);
            model.Add("recipes", recipesByCategory);
            return View("Show", model);

        }
    }
}
