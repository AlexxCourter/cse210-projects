public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, string points, bool isComplete=false) : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public override void RecordEvent()
    {
        //can use the base because there are no other variables but the base class
        //ones to worry about.
        _isComplete = true;
        base.RecordEvent();
    }

    public override bool IsComplete()
    {
        return _isComplete;
    }

    public override string GetStringRepresentation()
    {
        string result = "SimpleGoal:";
        if (IsComplete())
        {
            result += base.GetStringRepresentation();
            result += "||true";
        }
        else
        {
            result += base.GetStringRepresentation();
            result += "||false";
        }
        return result;
    }
}