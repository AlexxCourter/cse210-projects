
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

    public override void Display()
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

            Console.Write("\n\nType number of item to mark, or quit to exit: ");
            string choice = Console.ReadLine();

            int result;
            bool convertable = int.TryParse(choice, out result);

            if (choice.ToLower() == "quit")
            {
                runChecklist = false;
            }
            else if (convertable && result >= 0)
            {
                _activeList.MarkItem(result-1);
            }
            else
            {
                Console.Clear();
                Console.Write("Woops! Try typing the number next to the item you want to mark. Or, type quit if you are done (press enter to return to list).");
                Console.ReadLine();
            }
        }
    }
    public override ShoppingList EditActive()
    {
        ShoppingList result = _activeList;
        //TODO: need logic for displaying edit menu to the user
        return result;
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