using System;

namespace Maze
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            MazeBuilder maze = new MazeBuilder(10,10,3);
            Console.WriteLine("Maze Length:" +maze.Maze.Length);
            Console.ReadKey();
        }
    }
}
