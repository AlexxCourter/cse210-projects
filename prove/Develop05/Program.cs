using System;

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new();
        manager.Start();

        //creative additions include:
        //GoalManager: CheckLevel() is a simple level up system that counts points and outputs a level.
        //GoalManager: GetAchievements() - checks number of goals completed and outputs a badge type.
    }
}