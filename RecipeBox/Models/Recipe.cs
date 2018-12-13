using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RecipeBox;

namespace RecipeBox.Models
{
    public class Recipe
    {
        private int _id;
        private string _name;
        private string _instructions;
        private int _rating;

        public Recipe(string name, string instructions, int rating, int id = 0)
        {
            _id = id;
            _name = name;
            _instructions = instructions;
            _rating = rating;
        }
        public string GetName()
        {
            return _name;
        }
        public string GetInstructions()
        {
            return _instructions;
        }
        public int GetRating()
        {
            return _rating;
        }
        public int GetId()
        {
            return _id;
        }
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO recipes (name, instructions, rating) VALUES (@name, @instructions, @rating);";
            cmd.Parameters.AddWithValue("@name", this._name);
            cmd.Parameters.AddWithValue("@instructions", this._instructions);
            cmd.Parameters.AddWithValue("@rating", this._rating);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<Recipe> GetAll()
        {
            List<Recipe> allRecipes = new List<Recipe> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM recipes;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string instructions = rdr.GetString(2);
                int rating = rdr.GetInt32(3);
                Recipe newRecipe = new Recipe(name, instructions, rating, id);
                allRecipes.Add(newRecipe);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allRecipes;
        }
        public static Recipe Find(int recipeId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM recipes WHERE id = (@recipe_id);";
            MySqlParameter recipe = new MySqlParameter();
            cmd.Parameters.AddWithValue("@recipe_id", recipeId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int id = 0;
            string name = "";
            string instructions = "";
            int rating = 0;
            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                name = rdr.GetString(1);
                instructions = rdr.GetString(2);
                rating = rdr.GetInt32(3);

            }
            Recipe foundRecipe = new Recipe(name, instructions, rating, id);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundRecipe;
        }
        public void AddCategory(int categoryId, int recipeId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO categories_recipes (category_id, recipe_id) VALUES (@categoryId, @recipeId);";
            MySqlParameter category_id = new MySqlParameter();
            category_id.ParameterName = "@categoryId";
            category_id.Value = categoryId;
            cmd.Parameters.Add(category_id);
            MySqlParameter recipe_id = new MySqlParameter();
            recipe_id.ParameterName = "@recipeId";
            recipe_id.Value = recipeId;
            cmd.Parameters.Add(recipe_id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public List<Category> FindCategoryOfRecipe(int recipeId)
        {
            List<Category> recipeCategories = new List<Category> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT categories.* FROM
                categories JOIN categories_recipes ON (categories.id = categories_recipes.category_id)
                WHERE categories_recipes.recipe_id =" + recipeId + ";";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int categoryId = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Category recipeCategory = new Category(name, categoryId);
                recipeCategories.Add(recipeCategory);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return recipeCategories;
        }

        public void Edit(string name, string instructions, int rating)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE recipes SET name = @name, instructions = @instructions, rating = @rating WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@instructions", instructions);
            cmd.Parameters.AddWithValue("@rating", rating);

            cmd.ExecuteNonQuery();
            _name = name;
            _instructions = instructions;
            _rating = rating;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM recipes;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<Recipe> SortByRating()
        {
            List<Recipe> sortedRecipes = new List<Recipe> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM recipes ORDER BY rating DESC;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string instructions = rdr.GetString(2);
                int rating = rdr.GetInt32(3);

                Recipe newRecipe = new Recipe(name, instructions, rating, id);
                sortedRecipes.Add(newRecipe);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return sortedRecipes;
        }

        public void DeleteRecipe(int recipeId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM recipes WHERE id = @recipeId; DELETE FROM categories_recipes WHERE recipe_id = @recipeId;";
            MySqlParameter recipeIdParameter = new MySqlParameter();
            recipeIdParameter.ParameterName = "@recipeId";
            recipeIdParameter.Value = recipeId;
            cmd.Parameters.Add(recipeIdParameter);
            cmd.ExecuteNonQuery();
            if (conn != null)
            {
                conn.Close();
            }
        }

    }
    // ************************* Join table methods are below ***************************
    public class JoinRecipeIngredient
    {
        private int _id;
        private int _ingredient_id;
        private int _recipe_id;

        public JoinRecipeIngredient(int ingredientId, int recipeId, int id = 0)
        {
            _ingredient_id = ingredientId;
            _recipe_id = recipeId;
            _id = id;
        }
        public int GetId()
        {
            return _id;
        }

        public int GetIngredientId()
        {
            return _ingredient_id;
        }

        public int GetRecipeId()
        {
            return _recipe_id;
        }
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO recipes_ingredients(recipe_id, ingredient_id) VALUES (@recipeId, @ingredientId);";
            cmd.Parameters.AddWithValue("@ingredientId", this._ingredient_id);
            cmd.Parameters.AddWithValue("@recipeId", this._recipe_id);

            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static void RemoveIngredientFromRecipe(int ingredientId, int recipeId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM recipes_ingredients WHERE recipe_id = @recipeID AND ingredient_id = @ingredientId;";
            cmd.Parameters.AddWithValue("@ingredientId", ingredientId);
            cmd.Parameters.AddWithValue("@recipeId", recipeId);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Recipe> GetRecipesByIngredient(int ingredientId)
        {

            List<Recipe> recipesByIngredient = new List<Recipe> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT recipes.* FROM
                recipes JOIN recipes_ingredients ON (recipes.id = recipes_ingredients.recipe_id)
                    JOIN ingredients ON (recipes_ingredients.ingredient_id = ingredients.id)
                WHERE ingredients.id = " + ingredientId + ";";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string instructions = rdr.GetString(2);
                int rating = rdr.GetInt32(3);
                Recipe newRecipe = new Recipe(name, instructions, rating, id);
                recipesByIngredient.Add(newRecipe);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return recipesByIngredient;
        }

        public static List<Ingredient> GetIngredientsByRecipe(int recipeId)
        {

            List<Ingredient> IngredientsByRecipe = new List<Ingredient> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT ingredients.* FROM
                recipes JOIN recipes_ingredients ON (recipes.id = recipes_ingredients.recipe_id)
                    JOIN ingredients ON (recipes_ingredients.ingredient_id = ingredients.id)
                WHERE recipes.id = " + recipeId + ";";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Ingredient newIngredient = new Ingredient(name, id);
                IngredientsByRecipe.Add(newIngredient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return IngredientsByRecipe;
        }

    }
}
