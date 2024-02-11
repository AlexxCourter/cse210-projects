public class Goal
{
    private string _name;
    private string _description;
    private string _points;

    public Goal(string name, string description, string points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public virtual void RecordEvent()
    {
        Console.WriteLine($"Congratulations! You have earned {_points} points!");
    }

    public virtual bool IsComplete()
    {
        //eternal goal is used as the base functionality here.
        return false;
    }

    public virtual string GetDetailsString()
    {
        return $"{_name} ({_description})";
    }

//FOR SAVING/LOADING
    public virtual string GetStringRepresentation()
    {
        return $"{_name}||{_description}||{_points}";
    }

// for the get names function in goal manager.
    public string GetName()
    {
        return _name;
    }

    public virtual string GetPoints()
    {
        return _points;
    }
}