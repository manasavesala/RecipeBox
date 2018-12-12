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
            Dictionary<string, object> model = new Dictionary<string, object> { };
            List<Recipe> allRecipes = Recipe.GetAll();
            List<Ingredient> allIngredients = Ingredient.GetAll();
            model.Add("recipes", allRecipes);
            model.Add("ingredients", allIngredients);
            return View("New", model);
        }
        [HttpPost("/addRecipe")]
        public ActionResult Create(string name, string instructions, int rating)
        {
            Recipe newRecipe = new Recipe(name, instructions, rating);
            newRecipe.Save();
            return RedirectToAction("New");
        }
        [HttpPost("/recipe/assign")]
        public ActionResult Assign(int ingredient, int recipe)
        {
            JoinRecipeIngredient assigned = new JoinRecipeIngredient(ingredient, recipe);
            assigned.Save();
            return RedirectToAction("New");
        }
    }
}
