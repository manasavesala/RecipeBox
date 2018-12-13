using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RecipeBox;

namespace RecipeBox.Models
{
    public class Ingredient
    {
        private int _id;
        private string _name;

        public Ingredient(string name, int id = 0)
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
            cmd.CommandText = @"INSERT INTO ingredients (name) VALUES (@name);";
            cmd.Parameters.AddWithValue("@name", this._name);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<Ingredient> GetAll()
        {
            List<Ingredient> allIngredients = new List<Ingredient> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM ingredients;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Ingredient newIngredient = new Ingredient(name, id);
                allIngredients.Add(newIngredient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allIngredients;
        }

    }
}
