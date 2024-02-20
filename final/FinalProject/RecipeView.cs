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

    public override Recipe Display()
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
        int counter = 1;
        foreach(string direction in directions)
        {
            Console.WriteLine($"{counter}. {direction}");
            counter++;
        }

        Console.WriteLine("\nType edit to change this recipe, or press enter to return to main menu.");
        string editor = Console.ReadLine();
        if (editor == "edit")
        {
            EditActive();
        }
        return _activeRecipe;
    }

    public override void EditActive()
    {
        //gathers user input to edit an existing recipe
        //starts an edit loop
        bool editing = true;
        while(editing)
        {
            string activeData = _activeRecipe.GetSaveData(); //gets the save data to split up into the separate parts,
            //these parts can be chosen to overwrite in the edit loop.
            string[] parts = activeData.Split("::");
            string editName = parts[1];
            parts[2] = parts[2].Trim(new char[]{']', '=', '['}); //last delimiter gets trimmed for directions. avoids blank list items.
            string[] sections = parts[2].Split("[-]");
            sections[0] = sections[0].Trim(new char[]{']', '=', '['}); //last delimiter gets trimmed for ingredients.
            string[] ingredients = sections[0].Split("[=]");
            string[] directions = sections[1].Split("[=]");
            //we can now use the editName, and the lists of ingredients and directions to edit a new copy of the recipe.
            //this will be iterative through the loop, so we can see changes in real time as we choose to make more.
            Console.WriteLine($"Editing the {editName} recipe.");
            Console.WriteLine("1. Edit ingredients");
            Console.WriteLine("2. Edit directions");
            Console.WriteLine("3. Edit name of recipe");
            Console.WriteLine("4. Done");
            Console.Write("> ");
            string choice = Console.ReadLine();

            int counter = 1;

            switch(choice)
            {
                case "1":
                    foreach(string detail in _activeRecipe.GetIngredientDetails())
                    {
                        Console.WriteLine($"{counter}. {detail}");
                        counter++;
                    }
                    Console.Write("Enter the number of the ingredient you will change: ");
                    string selectedDetail = Console.ReadLine();
                    int ingredientIndex;
                    bool convertable = int.TryParse(selectedDetail, out ingredientIndex);
                    if (convertable && ingredientIndex > 0)
                    {
                        Console.Clear();
                        Console.WriteLine("What part of this ingredient needs to be changed?");
                        //save the separate parts of the ingredient that can be changed
                        //these will be inserted into a new Ingredient object, which will then replace
                        //the same in the original recipe object.
                        string chosenIngredient = ingredients[ingredientIndex - 1];
                        string[] ingParts = chosenIngredient.Split("||");
                        bool editParts = true;
                        do
                        {
                            counter = 1;
                            List<string> types = new(){"[Name]", "[Amount]", "[Measurement]", "[Checkmarked]"};
                            foreach (string part in ingParts)
                            {
                                Console.WriteLine($"  {counter}. {types[counter-1]} {part}");
                                counter++;
                            }
                            Console.Write("> ");
                            string selectedIngPart = Console.ReadLine();
                            int selectedPartIndex;
                            //overwrite for a new validity check.
                            convertable = int.TryParse(selectedIngPart, out selectedPartIndex);
                            if (convertable && selectedPartIndex > 0)
                            {
                                Console.WriteLine($"The old value was: {ingParts[selectedPartIndex-1]}");
                                Console.Write("\nEnter a new value: ");
                                ingParts[selectedPartIndex-1] = Console.ReadLine();
                                //update the ingredient
                                Ingredient updatedIngredient = new(ingParts[0],int.Parse(ingParts[1]),ingParts[2], bool.Parse(ingParts[3]));
                                _activeRecipe.UpdateIngredient(updatedIngredient, ingredientIndex-1);
                                //perform the update and exit the inner update loop, or continue editing.
                                Console.WriteLine("Edit another part of this ingredient? (type yes or no)");
                                Console.Write("> ");
                                string continueEdit = Console.ReadLine();
                                bool checker = true;
                                while (checker)
                                {
                                    switch(continueEdit)
                                    {
                                        case "yes":
                                            //continues through the edit ingredient loop once more.
                                            checker = false;
                                            break;
                                        case "no":
                                            checker = false;
                                            editParts = false;
                                            break;
                                        default:
                                            Console.WriteLine("Woops! Please only type the word yes or no.");
                                            Console.WriteLine("Edit another part of this ingredient?");
                                            Console.Write("> ");
                                            continueEdit = Console.ReadLine();
                                            Console.Clear();
                                            break;
                                    }
                                }
                                
                            } else
                            {
                                Console.WriteLine("\nOops. Please try again and only enter the correct input. (Press enter to go back to the editor).");
                                Console.ReadLine();
                            }
                        } while (editParts);
                        


                    }
                    break;
                case "2":
                    counter = 1;
                    foreach(string detail in _activeRecipe.GetDirections())
                    {
                        Console.WriteLine($"{counter}. {detail}");
                        counter++;
                    }
                    Console.WriteLine("Enter the number of the direction you will change: ");
                    string chosenDirection = Console.ReadLine();
                    int directionIndex;
                    convertable = int.TryParse(chosenDirection, out directionIndex);

                    if (convertable && directionIndex > 0)
                    {
                        string editDirection = directions[directionIndex -1];
                        Console.Clear();
                        Console.WriteLine($"Old value: {editDirection}");
                        Console.Write("\nEnter a new value for this step of the recipe:");
                        editDirection = Console.ReadLine();
                        _activeRecipe.UpdateDirections(editDirection, directionIndex-1);
                    }
                    break;
                case "3":
                    Console.Write("\nEnter a new name for this recipe: ");
                    editName = Console.ReadLine();
                    _activeRecipe.UpdateName(editName);
                    break;
                case "4":
                    editing = false;
                    break;
                default:
                    Console.WriteLine("Woops! Please only enter one of the numbers displayed.");
                    break;
            }
        }
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