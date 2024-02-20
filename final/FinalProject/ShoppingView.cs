
public class ShoppingView : PageView
{
    private ShoppingList _activeList;
    private int _index;

    public ShoppingView()
    {
        //used when creating the first in the list. Blank will get replaced by the new list.
        ShoppingList blank = new();
        _activeList = blank;
        _index = 0;
    }

    public ShoppingView(ShoppingList list, int index)
    {
        _activeList = list;
        _index = index;
    }

    public override ShoppingList Display()
    {
        bool runChecklist = true;

        while(runChecklist == true)
        {
            List<string> details = _activeList.GetIngredientChecklist();
            Console.Clear();

            Console.WriteLine($"List from {_activeList.GetTimeStamp()}\n");

            int counter = 1;
            foreach(string detail in details)
            {
                Console.WriteLine($"{counter}. {detail}");
                counter++;
            }

            Console.Write("\n\nType number of item to mark, edit to edit list, or quit to exit: ");
            string choice = Console.ReadLine();

            int result;
            bool convertable = int.TryParse(choice, out result);

            if (choice.ToLower() == "quit")
            {
                runChecklist = false;
            }
            else if (choice.ToLower() == "edit")
            {
                EditActive();
            }
            else if (convertable && result >= 0)
            {
                _activeList.MarkItem(result-1);
            }
            else
            {
                Console.Clear();
                Console.Write("Woops! Try typing the number next to the item you want to mark, type edit if you want to edit the list, or, type quit if you are done (press enter to return to list).");
                Console.ReadLine();
            }
        }
        return _activeList;
    }
    public override void EditActive()
    {
        ShoppingList result = _activeList;
        //gathers user input to edit an existing recipe
        //starts an edit loop
        bool editing = true;
        while(editing)
        {
            List<Ingredient> ingredients = _activeList.GetIngredients();
            string timestamp = _activeList.GetTimeStamp();
            //we can now use the editName, and the lists of ingredients and directions to edit a new copy of the recipe.
            //this will be iterative through the loop, so we can see changes in real time as we choose to make more.
            Console.WriteLine($"Editing the Shopping list from {timestamp}.");
            Console.WriteLine("1. Edit ingredients");
            Console.WriteLine("2. Done");
            Console.Write("> ");
            string choice = Console.ReadLine();

            int counter = 1;

            switch(choice)
            {
                case "1":
                    foreach(string detail in _activeList.GetIngredientDetails())
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
                        Ingredient chosenIngredient = ingredients[ingredientIndex - 1];
                        bool editParts = true;
                        do
                        {
                            counter = 1;
                            List<string> types = new(){"[Name]", "[Amount]", "[Measurement]", "[Checkmarked]"};
                            string[] ingParts = chosenIngredient.GetSaveable().Split("||");
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
                                _activeList.UpdateIngredient(updatedIngredient, ingredientIndex-1);
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
                    editing = false;
                    break;
                default:
                    Console.WriteLine("Woops! Please only enter one of the numbers displayed.");
                    break;
            }
        }
    }

    public ShoppingList CreateNew(int index)
    {
        //provides the user entry capture logic. This will pass the recipe back to the controller for saving.
        //for future versions of the app that may allow viewing the most recent viewed/created recipe, sets
        //this new recipe as the active using the index passed to function.
        //note: the index passed here should always be BookController._recipes.Count before the new recipe is added.
        //because this will be equal to the current indexes [0 to n] + 1;
        Console.Clear();
        ShoppingList result = new();
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

            newIngredient = new(ingName, amount);     
            result.AddIngredient(newIngredient); 
            
            // bool validMeasure = false;
            // do
            // {
                
            //     Console.WriteLine($"Does ingredient #{counter} use a measurement, such as 'cups', 'ounces', or 'tablespoons'?");
            //     Console.Write("Type yes or no: ");
            //     string measureCheck = Console.ReadLine();
            //     if ( measureCheck == "yes")
            //     {
            //         validMeasure = true;
            //         Console.Write($"What measurement (cups, oz., tablespoons) does ingredient #{counter} use? ");
            //         string measurement = Console.ReadLine();
            //         newIngredient = new(ingName, amount, measurement);
            //         result.AddIngredient(newIngredient);


            //     } else if ( measureCheck == "no")
            //     {
            //         validMeasure = true;
            //         newIngredient = new(ingName, amount);     
            //         result.AddIngredient(newIngredient);            

            //     } else 
            //     {
            //         Console.Clear();
            //         Console.WriteLine("Sorry, there must have been a typo. Please only type yes or no.");
            //     }
            // } while (validMeasure == false);
            
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

        _activeList = result;
        _index = index;
        //finally, send out the new List.
        return result;
    }

    public void SetActive(ShoppingList list)
    {
        _activeList = list;
    }


}