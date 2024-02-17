using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;

public class RecipeView : PageView
{
    private int _index;
    private Recipe _activeRecipe;

    public RecipeView()
    {
        //used when creating the first recipe.
        Recipe blank = new("placeholder");
        _activeRecipe = blank;
        _index = 0;
    }

    public RecipeView(Recipe active, int index)
    {
        _activeRecipe = active;
        _index = index;
    }

    public override void Display()
    {
        Console.Clear();

        List<string> details = _activeRecipe.GetIngredientDetails();
        List<string> directions = _activeRecipe.GetDirections();

        Console.WriteLine($"{_activeRecipe.GetName()}\n");
        Console.WriteLine("Ingredients:");
        foreach(string detail in details)
        {
            Console.WriteLine(detail);
        }

        Console.WriteLine("\nInstructions:");

        foreach(string direction in directions)
        {
            Console.WriteLine(direction);
        }

        Console.WriteLine("\nPress enter to return to main menu.");
        Console.ReadLine();
    }

    public override Recipe EditActive()
    {
        //gathers user input to edit an existing recipe
        //STUB
        return _activeRecipe;
    }

    public Recipe CreateNew(int index)
    {
        //provides the user entry capture logic. This will pass the recipe back to the controller for saving.
        //for future versions of the app that may allow viewing the most recent viewed/created recipe, sets
        //this new recipe as the active using the index passed to function.
        //note: the index passed here should always be BookController._recipes.Count before the new recipe is added.
        //because this will be equal to the current indexes [0 to n] + 1;
        string name;
        Console.Clear();

        Console.Write("Enter the name of the recipe: ");
        name = Console.ReadLine();
        Recipe result = new(name);
        //ingredient creation loop.
        bool ingLoop = true;
        int counter = 1;
        while (ingLoop)
        {
            Console.Clear();
            Ingredient newIngredient;
            //create each part of the ingredient
            Console.Write($"Enter the name of ingredient #{counter}: ");
            string ingName = Console.ReadLine();
            int amount;
            bool valid = false;
            do
            {
                Console.Write($"Enter the amount or quantity for ingredient #{counter}: ");
                string response = Console.ReadLine();
                valid = int.TryParse(response, out amount);
                if(valid == false)
                {
                    Console.Clear();
                    Console.WriteLine("Please only enter a number, such as 2 for 2 carrots or 2 cups flour");
                }
            } while (valid == false);
            
            bool validMeasure = false;
            do
            {
                
                Console.WriteLine($"Does ingredient #{counter} use a measurement, such as 'cups', 'ounces', or 'tablespoons'?");
                Console.Write("Type yes or no: ");
                string measureCheck = Console.ReadLine();
                if ( measureCheck == "yes")
                {
                    validMeasure = true;
                    Console.Write($"What measurement (cups, oz., tablespoons) does ingredient #{counter} use? ");
                    string measurement = Console.ReadLine();
                    newIngredient = new(ingName, amount, measurement);
                    result.AddIngredient(newIngredient);


                } else if ( measureCheck == "no")
                {
                    validMeasure = true;
                    newIngredient = new(ingName, amount);     
                    result.AddIngredient(newIngredient);            

                } else 
                {
                    Console.Clear();
                    Console.WriteLine("Sorry, there must have been a typo. Please only type yes or no.");
                }
            } while (validMeasure == false);
            
            bool next = false;
            do
            {
                Console.WriteLine("Add more ingredients? type yes or no: ");
                Console.Write("> ");
                string addMore = Console.ReadLine();
                if (addMore == "yes")
                {
                    //continues the loop.
                    counter++;
                    next = true;
                }
                else if (addMore == "no")
                {
                    //exits the loop
                    ingLoop = false;
                    next = true;
                }
                else
                {
                    Console.WriteLine("Woops! Please only type the word yes or no.");
                }
            } while (next == false);
        }

        //directions loop
        counter = 1; //reset the counter
        bool dirLoop = true;
        while (dirLoop)
        {
            Console.Clear();
            Console.WriteLine("Write the instructions for this recipe one at a time, in order.");
            Console.Write($"Step #{counter}: ");
            string newDirection = Console.ReadLine();
            result.AddDirection(newDirection);

            bool next = false;
            do
            {
                Console.WriteLine("Add another step? type yes or no: ");
                Console.Write("> ");
                string addMore = Console.ReadLine();
                if (addMore == "yes")
                {
                    //continues the loop.
                    counter++;
                    next = true;
                }
                else if (addMore == "no")
                {
                    //exits the loop
                    dirLoop = false;
                    next = true;
                }
                else
                {
                    Console.WriteLine("Woops! Please only type the word yes or no.");
                }
            } while (next == false);
        }
        //set this view with the new recipe as active
        _activeRecipe = result;
        _index = index;
        //finally, send out the new Recipe.
        return result;
    }
}