# _RecipeBox.Solutions_

#### _MVC web application ,C# project._

#### By _**Manasa Vesala**_

## Description

_MVC web application for a user, where the  user should be able to add, update and remove a list of the recipes and save that data into database._

## Specifications

_For recipe box_

* _As a user, You can add a recipe with ingredients and instructions, so you can remember how to prepare your favorite dishes._
* _As a user, You can tag my recipes with different categories, so recipes are easier to find. A recipe can have many tags and a tag can have many recipes._
* _As a user, You can be able to update and delete tags, so you can have flexibility with how you categorize recipes._
* _As a user, You can edit recipes, so you can make improvements or corrections to my recipes._
* _As a user, You can be able to delete recipes you don't like or use, so you don't have to see them as choices._
* _As a user, You can rate recipes, so you know which ones are the best.
* _As a user, You can list recipes by highest rated so you can see which ones you like the best._
* _As a user, You can see all recipes that use a certain ingredient, so you can more easily find recipes for the ingredients you have._

## Setup/Installation Requirements

1. _Clone this repository -https://github.com/manasavesala/RecipeBox.git_
2. _In MySQL_
   * CREATE DATABASE manasa_vesala;
   * USE manasa_vesala;
   * CREATE TABLE Recipies (id serial PRIMARY KEY, name VARCHAR(255));
   * CREATE TABLE Ingerdients (id serial PRIMARY KEY, description VARCHAR(255));
3. _In Terminal Change into the work directory:: $ cd RecipeBox.Solution_
4. _To edit the project, open the project in your preferred text editor._
5. _To run the program, first navigate to the location of the RecipeBox.cs file_ 
6. _To run the program: $ cd RecipeBox $ cd dotnet build $cd dotnet run_
7. _To run the tests, use these commands: $ cd RecipeBox.Tests $ dotnet test_
8. _Navigate to http://localhost:5000 in your browser to view the splashpage._

## Support and contact details

_Contact Manasa Vesala - vesalamanasa@gmail.com_


## Technologies Used

* _C#_
* _.NET_
* _MySQL_
* _Atom_
* _Git_
* _GitHub_
* _MAMP_
* _HTML_
* _BootStrap_

### License

*This software is licensed under the MIT license.*

Copyright (c) 2018 **_Manasa Vesala_**