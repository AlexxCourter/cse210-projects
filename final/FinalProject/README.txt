Recipe Helper
By Alexander Courter
This project was created as an open-ended final project for CSE 210
Version 1.0.0
Last update: 2/20/2024


## Instructions:
Running the FinalProject application from the run and debug menu will open
the terminal with prompts for you to interact with the recipe book. Initially,
data will not be loaded into the app. if you have a recipebook.txt file already, select option
6 to load your previously saved data from that file. If you do not have this file, simply use the app
to create new recipes or lists and follow the on-screen prompts. Be sure to select 5 from the menu to save your
data when you are finished before you select quit. Selecting "Save Data" will write (or overwrite) a new
recipebook.txt file that stores the data of the application.

CLASSES:
  BookController - controls the application behaviors and holds the other class iterations, runs the menu loop, stores, saves, and loads data.
  DataModel - base class that designs how the basic data object will behave. ShoppingList matches several of the default behaviors while Recipe overrides them.
  Recipe - a subclass of DataModel that outlines a recipe object.
  ShoppingList - a subclass of DataModel that outlines a shopping list object.
  PageView - an abstract base class that defines several required behaviors for "view" classes.
  RecipeView - a subclass of PageView that displays, edits, or prompts the user in creating new recipes.
  ShoppingView - a subclass of PageView that displays, edits, or prompts the user in creating new shopping lists.
  Ingredient - an abstraction of a single ingredient, the base object of both recipes and shopping lists. Defines the core data needed for one ingredient.


