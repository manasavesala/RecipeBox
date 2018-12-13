using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RecipeBox;

namespace RecipeBox.Models
{
    public class Category
    {
        private int _id;
        private string _name;

        public Category(string name, int id = 0)
        {
            _id = id;
            _name = name;
        }
        public string GetName()
        {
            return _name;
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
            cmd.CommandText = @"INSERT INTO Categories (name) VALUES (@name);";
            cmd.Parameters.AddWithValue("@name", this._name);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<Category> GetAll()
        {
            List<Category> allCategories = new List<Category> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM categories;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Category newCategory = new Category(name, id);
                allCategories.Add(newCategory);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCategories;
        }
        public static Category Find(int categoryId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM categories WHERE id = (@category_id);";
            MySqlParameter recipe = new MySqlParameter();
            cmd.Parameters.AddWithValue("@category_id", categoryId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int id = 0;
            string name = "";
            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                name = rdr.GetString(1);
            }
            Category foundCategory = new Category(name, id);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundCategory;
        }

        public static List<Recipe> FindRecipesOfCategory(int CategoryId)
        {
            List<Recipe> recipeCategories = new List<Recipe> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT recipes.* FROM
                recipes JOIN categories_recipes ON (recipes.id = categories_recipes.recipe_id)
                WHERE categories_recipes.category_id =" + CategoryId + ";";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int Id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string instructions = rdr.GetString(2);
                int rating = rdr.GetInt32(3);
                Recipe recipeRecipe = new Recipe(name, instructions, rating, Id);
                recipeCategories.Add(recipeRecipe);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return recipeCategories;
        }
    }
}
