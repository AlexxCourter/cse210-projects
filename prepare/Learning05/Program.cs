using System;

class Program
{
    static void Main(string[] args)
    {
        //call iterations of the shape classes
        Square sq = new("red", 5.5);
        Rectangle rec = new("blue", 2, 4);
        Circle circ = new("green", 6);

        List<Shape> shapes = new(){
            sq,
            rec,
            circ
        };

        foreach(Shape shape in shapes)
        {
        Console.WriteLine(shape.GetColor());
        Console.WriteLine(shape.GetArea());
        Console.WriteLine(); //spacer
        }
    }
}