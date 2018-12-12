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
            List<Category> allCategories = Category.GetAll();
            model.Add("recipes", allRecipes);
            model.Add("ingredients", allIngredients);
            model.Add("categories", allCategories);
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
        [HttpPost("/recipe/assignCategory")]
        public ActionResult Categorize(int category, int recipe)
        {
            Recipe selectedRecipe = Recipe.Find(recipe);
            selectedRecipe.AddCategory(category, recipe);
            return RedirectToAction("New");
        }
        [HttpGet("recipe/{recipeId}/details")]
        public ActionResult Show(int recipeId)
        {
            Dictionary<string, object> model = new Dictionary<string, object> { };
            Recipe selectedRecipe = Recipe.Find(recipeId);
            List<Ingredient> ingredientsForRecipe = JoinRecipeIngredient.GetIngredientsByRecipe(recipeId);
            List<Category> categoriesOfRecipe = selectedRecipe.FindCategoryOfRecipe(recipeId);
            model.Add("recipe", selectedRecipe);
            model.Add("ingredients", ingredientsForRecipe);
            model.Add("categories", categoriesOfRecipe);
            return View("Detail", model);
        }
    }
}
