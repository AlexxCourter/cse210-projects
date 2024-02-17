using System.Diagnostics;

public class BookController
{
    private List<ShoppingList> _shoppingLists;
    private List<Recipe> _recipes;

    private ShoppingView _shopView = new();
    private RecipeView _recView = new();

    public BookController()
    {
        _shoppingLists = new();
        _recipes = new();

    }

    public void Run()
    {
        Console.WriteLine("MILESTONE VERSION: This version of the app successfully creates new recipes/shopping lists & displays them.");
        //runs the main loop that serves the recipe book
        bool runningState = true;

        while (runningState == true)
        {
             //menu gets the user's choice.
             string choice = Menu();
             int selectList; // indicates what list is selected as the active.
             int selectRecipe; // indicates what recipe is selected as the active.

             switch (choice)
             {
                case "1":
                    selectList = DisplayShoppingLists();
                    if (selectList >= 0)
                    {
                        _shopView = new(_shoppingLists[selectList], selectList);
                        _shopView.Display();
                    }
                    break;
                case "2":
                    selectRecipe = DisplayRecipes();
                    if (selectRecipe >= 0)
                    {
                        _recView = new(_recipes[selectRecipe], selectRecipe);
                        _recView.Display();
                    }
                    break;
                case "3":
                    _shoppingLists.Add(_shopView.CreateNew(_shoppingLists.Count));
                    break;
                case "4":
                    _recipes.Add(_recView.CreateNew(_recipes.Count));
                    break;
                case "5":
                    runningState = false;
                    break;
                default:
                    Console.WriteLine("Woops! Incorrect entry. Try entering just the number next to the menu option you want.");
                    break;
             }

        }
       

    }

    public string Menu()
    {
        Console.WriteLine("Select an option by typing the number:");
        Console.WriteLine("1. Display Shopping Lists");
        Console.WriteLine("2. Display Recipes");
        Console.WriteLine("3. New shopping list");
        Console.WriteLine("4. New recipe");
        Console.WriteLine("5. Quit");
        Console.Write("> ");
        string choice = Console.ReadLine();
        return choice;
    }

    public void Save()
    {
        //saves the current data to a file. Name and contents of this file are not revealed to user when saving, this will be like an internal data file.
        string filename = "recipebook.txt";
        //file writing code adapted from the lesson material at https://byui-cse.github.io/cse210-ww-course-2023/week05/develop.html
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (ShoppingList list in _shoppingLists)
            {
                outputFile.WriteLine(list.GetSaveData());
            }
            foreach(Recipe rec in _recipes)
            {
                outputFile.WriteLine(rec.GetSaveData());
            }
        }
    }

    public void Load()
    {
        //loads data from an internal data file.
        //DELIMITER EXPLANATION
        // :: - separates the name/timestamp of recipe/shoppingList from the data
        //[+] - separates the shopping lists (top) from the recipes (bottom) data
        //[=] - separates each detail, for a recipe, this separates first the ingredients and then the directions.
        //[-] - separates the ingredients from the directions in a recipe.
        // || - separates parts of an ingredient.

    }

    public int DisplayShoppingLists()
    {
        //if there are no lists, return with -1 to indicate no action.
        if (_shoppingLists.Count < 1)
        {
            Console.WriteLine("No shopping lists found. Load or create a shopping list first!");
            return -1;
        }
        //displays all the shopping lists
        int counter = 1;
        foreach (ShoppingList list in _shoppingLists)
        {
            Console.WriteLine($"{counter}. {list.GetTimeStamp()}");
            counter++;
        }
        Console.Write("\nType number of list you want to view or press enter to go back. ");
        string choice = Console.ReadLine();
        if (choice == "")
        {
            return -1; // will indicate that the program should go back to main menu
        }
        int result;
        bool check = int.TryParse(choice, out result);
        while(check == false | result < 0)
        {
            Console.Write("Something went wrong. Either press enter to go back or type the number you see next to the list. ");
            choice = Console.ReadLine();
            check = int.TryParse(choice, out result);
             if (choice == "")
            {
                check = true;
                return -1; // will indicate that the program should go back to main menu
            }
        }
        return result-1;
    }

    public int DisplayRecipes()
    {
        //if there are no recipes, return with -1 to indicate no action.
        if (_recipes.Count < 1)
        {
            Console.WriteLine("No recipes found. Load or create a recipe first!");
            return -1;
        }
        //displays all the shopping lists
        int counter = 1;
        foreach (Recipe item in _recipes)
        {
            Console.WriteLine($"{counter}. {item.GetName()}");
            counter++;
        }
        Console.Write("\nType number of recipe you want to view or press enter to go back. ");
        string choice = Console.ReadLine();
        if (choice == "")
        {
            return -1; // will indicate that the program should go back to main menu
        }
        int result;
        bool check = int.TryParse(choice, out result);
        while(check == false | result < 0)
        {
            Console.Write("Something went wrong. Either press enter to go back or type the number you see next to the recipe. ");
            choice = Console.ReadLine();
            check = int.TryParse(choice, out result);
             if (choice == "")
            {
                check = true;
                return -1; // will indicate that the program should go back to main menu
            }
        }
        return result -1;
    }


    public void AddNewRecipe(Recipe recipe)
    {
        _recipes.Add(recipe);
    }

    public void AddNewShopList(ShoppingList list)
    {
        _shoppingLists.Add(list);
    }
}