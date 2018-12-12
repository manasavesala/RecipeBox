using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;

namespace RecipeBox.Controllers
{
    public class IngredientController : Controller
    {
        [HttpGet("/ingredient/new")]
        public ActionResult New()
        {
            List<Ingredient> allingredients = Ingredient.GetAll();
            return View("New", allingredients);
        }
        [HttpPost("/addIngredient")]
        public ActionResult Create(string ingredientName)
        {
            Ingredient newIngredient = new Ingredient(ingredientName);
            newIngredient.Save();
            return RedirectToAction("New");
        }
    }
}
