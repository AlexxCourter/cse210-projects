
public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name, string description, string points, int bonus, int target, int amountCompleted=0) : base(name, description, points)
    {
        _amountCompleted = amountCompleted;
        _target = target;
        _bonus = bonus;
    }

    public override void RecordEvent()
    {
        _amountCompleted += 1;
        if (IsComplete())
        {
            int total = int.Parse(GetPoints()) + _bonus;
            Console.WriteLine($"Congratulations! You have earned {total} points!");
        }
        base.RecordEvent();
    }

    public override bool IsComplete()
    {
        if (_amountCompleted >= _target)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override string GetDetailsString()
    {
        string details = base.GetDetailsString();
        details += $" -- Currently completed: {_amountCompleted}/{_target}";
        return details;
    }

    public override string GetStringRepresentation()
    {
        string goalString = "ChecklistGoal:";
        goalString += base.GetStringRepresentation();
        goalString += $"||{_bonus}||{_amountCompleted}||{_target}";
        return goalString;
    }

    public override string GetPoints()
    {
        if (IsComplete())
        {
            return base.GetPoints() + _bonus;
        }
        else
        {
            return base.GetPoints();
        }
    }
}