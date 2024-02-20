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
                        _shoppingLists[selectList] = _shopView.Display();
                    }
                    break;
                case "2":
                    selectRecipe = DisplayRecipes();
                    if (selectRecipe >= 0)
                    {
                        _recView = new(_recipes[selectRecipe], selectRecipe);
                        _recipes[selectRecipe] = _recView.Display();
                    }
                    break;
                case "3":
                    _shoppingLists.Add(_shopView.CreateNew(_shoppingLists.Count));
                    break;
                case "4":
                    _recipes.Add(_recView.CreateNew(_recipes.Count));
                    break;
                case "5":
                    Save();
                    break;
                case "6":
                    Load();
                    break;
                case "7":
                    SendToList();
                    break;
                case "8":
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
        Console.WriteLine("5. Save Data.");
        Console.WriteLine("6. Load Data.");
        Console.WriteLine("7. Send a recipe to Shopping List.");
        Console.WriteLine("8. Quit");
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
        // :: - separates the name/timestamp and type of object of recipe/shoppingList from the data
        //[=] - separates each detail, for a recipe, this separates first the ingredients and then the directions.
        //[-] - separates the ingredients from the directions in a recipe.
        // || - separates parts of an ingredient.
        string filename = "recipebook.txt";

        string[] lines = System.IO.File.ReadAllLines(filename);

        foreach(string line in lines)
        {
            //first, identify the type of object in data[0].
            string[] data = line.Split("::");
            string type = data[0];
            string name = data[1]; //contains name for recipe, timestamp for list.
            string details = data[2]; //contains all ingredients and directions for that line.
            string[] ingredients;

            switch(type)
            {
                case "[s]":
                    //set up the shopping list that will be loaded in
                    ShoppingList list = new(name); //in this case, name above is the loaded time stamp.
                    details = details.Trim(new Char[]{']', '=', '['}); //remove delimiter at ends to prevent empty lines
                    ingredients = details.Split("[=]");
                    foreach (string ingredient in ingredients)
                    {
                        // order is {_name}||{_quantity}||{_measurement}||{_checked}
                        string[] parts = ingredient.Split("||");
                        Ingredient newIngredient = new(parts[0], int.Parse(parts[1]), "", bool.Parse(parts[3])); //shopping lists don't track measurements.
                        list.AddIngredient(newIngredient);
                    }
                    _shoppingLists.Add(list);
                    
                    break;
                case "[r]":
                    Recipe recipe = new(name); //in this case, name above is the loaded time stamp.
                    details = details.Trim(new Char[]{']', '=', '['}); //remove delimiter at ends to prevent empty lines
                    string[] recSections = details.Split("[-]");
                    ingredients = recSections[0].Trim(new Char[]{']', '=', '['}).Split("[=]"); //need to remove any trailing delimiter again then split.
                    string[] directions = recSections[1].Split("[=]");
                    foreach (string ingredient in ingredients)
                    {
                        // order is {_name}||{_quantity}||{_measurement}||{_checked}
                        string[] parts = ingredient.Split("||");
                        string measurement = parts[2]; //need to parse any marked NONE.
                        if (measurement == "NONE")
                        {
                            measurement = ""; //fixes measurement for proper display.
                        }
                        Ingredient newIngredient = new(parts[0], int.Parse(parts[1]), measurement, bool.Parse(parts[3])); //shopping lists don't track measurements.
                        recipe.AddIngredient(newIngredient);
                    }
                    foreach (string direction in directions)
                    {
                        recipe.AddDirection(direction);
                    }
                    _recipes.Add(recipe);
                    break;
            }
        }

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

    public void SendToList()
    {
        int choice = DisplayRecipes();
        Console.WriteLine($"You will send the ingredients of {_recipes[choice].GetName()} to a new shopping list. Continue?");
        Console.Write("Type yes or no: ");
        string send = Console.ReadLine().ToLower();
        switch(send)
        {
            case "yes":
                ShoppingList newList = new();
                foreach(Ingredient ingredient in _recipes[choice].GetIngredients())
                {
                    newList.AddIngredient(ingredient);
                }
                AddNewShopList(newList);
                Console.WriteLine($"New shopping list created from {_recipes[choice].GetName} recipe.");
                break;
            case "no":
                break;
        }
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