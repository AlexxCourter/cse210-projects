public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    public GoalManager()
    {
        _score = 0;
        _goals = new();
    }

    public void Start()
    {
        bool running = true;
        
        while(running == true)
        {
            DisplayPlayerInfo();

            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create new goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Check Achievements");
            Console.WriteLine("  7. Quit");

            Console.Write("Select a choice from the menu: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    ListGoalDetails();
                    break;
                case "3":
                    SaveGoals();
                    break;
                case "4":
                    LoadGoals();
                    break;
                case "5":
                    RecordEvent();
                    break;
                case "6":
                    GetAchievments();
                    break;
                case "7":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Woops! Please enter a number from the menu, such as 2.");
                    break;
            }
        }
    }

    public void DisplayPlayerInfo()
    {
        Console.WriteLine($"\nYou have {_score} points.");
        //creative addition part 1: CheckLevel()
        Console.WriteLine($"You are level {CheckLevel()}.\n");
    }

    public void ListGoalNames()
    {
        int counter = 1;
        foreach (Goal goal in _goals)
        {
            Console.WriteLine($"{counter}. {goal.GetName()}");
            counter++;
        }
    }

    public void ListGoalDetails()
    {
        int counter = 1;
        foreach (Goal goal in _goals)
        {
            if (goal.IsComplete())
            {
                Console.WriteLine($"{counter}. [X] {goal.GetDetailsString()}");
            }
            else
            {
                Console.WriteLine($"{counter}. [ ] {goal.GetDetailsString()}");
            }
            counter++;
        }
    }

    public void CreateGoal()
    {
        Console.WriteLine("The types of Goals are:\n  1. Simple Goal\n  2. Eternal Goal\n  3. Checklist Goal");
        Console.Write("\nWhich type of goal would you like to create? ");
        string choice = Console.ReadLine();

        //can accept the default parameters before the switch.
        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();

        Console.Write("What is a short description of it? ");
        string desc = Console.ReadLine();

        Console.Write("What is the amount of points associated with this goal? ");
        string points = Console.ReadLine();

        switch (choice)
        {
            case "1":
                //creates a simple goal
                SimpleGoal newGoal = new(name, desc, points);
                _goals.Add(newGoal);
                break;
            case "2":
                //creates an Eternal goal
                EternalGoal newEternalGoal = new(name, desc, points);
                _goals.Add(newEternalGoal);
                break;
            case "3":
                Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                int target = int.Parse(Console.ReadLine());
                Console.Write($"How many bonus points will be awarded when this goal is completed {target} times? ");
                int bonus = int.Parse(Console.ReadLine());

                ChecklistGoal newChecklistGoal = new(name, desc, points, bonus, target);
                _goals.Add(newChecklistGoal);
                break;
        }
    }

    public void RecordEvent()
    {
        //selects the goal that will call its own RecordEvent
        Console.WriteLine("The goals are:");
        ListGoalNames();
        Console.Write("Which goal did you accomplish? ");
        int choice = int.Parse(Console.ReadLine());
        
        _goals[choice-1].RecordEvent();
        int points = int.Parse(_goals[choice-1].GetPoints());
        _score += points;
        Console.WriteLine($"You now have {_score} points.");


    }

    public void SaveGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();
        if (filename.EndsWith(".txt"))
        {
            //file writing code adapted from the lesson material at https://byui-cse.github.io/cse210-ww-course-2023/week05/develop.html
            using (StreamWriter outputFile = new StreamWriter(filename))
            {
                // You can add text to the file with the WriteLine method
                outputFile.WriteLine($"{_score}");
                foreach(Goal goal in _goals)
                {
                    outputFile.WriteLine(goal.GetStringRepresentation());
                }
            }
        }
        else
        {
            Console.WriteLine("Sorry. Please enter '.txt' as the filetype to save properly.");
        }

    }

    public void LoadGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();
        if (filename.EndsWith(".txt"))
        {
            //file reading code adapted from the lesson material at https://byui-cse.github.io/cse210-ww-course-2023/week05/develop.html
            string[] lines = System.IO.File.ReadAllLines(filename);
            string firstLine = lines.First();

            _score = int.Parse(firstLine);
            bool skip = true;

            foreach (string line in lines)
            {
                //my sneaky way of skipping the first line.
                //im sure there is a better way to remove, but
                //there is lack of built in functionality for string[] arrays.
                if (skip == true)
                {
                    //essentially wastes the first line of the loop, skipping the points.
                    skip = false;
                }
                else
                {
                    string[] parts = line.Split(":");

                    string goalType = parts[0];
                    string[] details = parts[1].Split("||");

                    switch (goalType)
                    {
                        case "SimpleGoal":
                            SimpleGoal goal = new(details[0], details[1], details[2], bool.Parse(details[3]));
                            _goals.Add(goal);
                            break;
                        case "EternalGoal":
                            EternalGoal etGoal = new(details[0], details[1], details[2]);
                            _goals.Add(etGoal);
                            break;
                        case "ChecklistGoal":
                            ChecklistGoal chGoal = new(details[0], details[1], details[2], int.Parse(details[3]), int.Parse(details[4]), int.Parse(details[5]));
                            _goals.Add(chGoal);
                            break;
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Sorry. Please enter '.txt' as the filetype to load properly.");
        }
    }

    //creative addition
    // level checker simply uses points to identify a level for the user.
    public int CheckLevel()
    {
        int level = 1;
        //
        if(_score >= 300 && _score < 650)
        {
            level = 2;
        }
        else if (_score >= 650 && _score < 1000)
        {
            level = 3;
        }
        else if (_score >= 1000 && _score < 1500)
        {
            level = 4;
        }
        else if (_score >= 1500 && _score < 2000)
        {
            level = 5;
        }
        else if (_score >= 2000 && _score < 2750)
        {
            level = 6;
        }
        else if (_score >= 2750)
        {
            level = 7;
        }
        return level;
    }

//creative addition
//simple achievement checker counts the number of completeable goals that are marked completed,
// and outputs an achievement for it.
    public void GetAchievments()
    {
        int counter = 0;
        foreach(Goal goal in _goals)
        {
            if(goal.IsComplete())
            {
                counter++;
            }
        }

        Console.WriteLine($"You have completed {counter} completeable goals so far.");
        if (counter >= 3 && counter < 6)
        {
            Console.WriteLine("You have earned the Bronze Achievement (3 goals)");
        } else if (counter >= 6 && counter < 10)
        {
            Console.WriteLine("You have earned the Silver Achievement (6 goals)");
        }
        if (counter >= 10)
        {
            Console.WriteLine("You have earned the Gold Achievement (10 goals)");
        }
    }
}