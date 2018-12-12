using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;

namespace RecipeBox.Controllers
{
    public class RecipeController : Controller
    {
        [HttpGet("/recipe/new")]
        public ActionResult New()
        {
            List<Recipe> allRecipes = Recipe.GetAll();
            return View("New", allRecipes);
        }
        [HttpPost("/addRecipe")]
        public ActionResult Create(string name, string instructions, int rating)
        {
            Recipe newRecipe = new Recipe(name, instructions, rating);
            newRecipe.Save();
            return RedirectToAction("New");
        }
    }
}
