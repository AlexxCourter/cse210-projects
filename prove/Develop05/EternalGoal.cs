public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, string points) : base(name, description, points)
    {

    }

    public override void RecordEvent()
    {
        base.RecordEvent();
    }

    public override bool IsComplete()
    {
        //eternal goals do not complete, but continue to provide points.
        //default returns false. See Goal.cs
        return base.IsComplete();
    }

    public override string GetStringRepresentation()
    {
        return "EternalGoal:" + base.GetStringRepresentation();
    }


}