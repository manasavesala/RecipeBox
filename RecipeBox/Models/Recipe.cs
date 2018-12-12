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
    }
}
