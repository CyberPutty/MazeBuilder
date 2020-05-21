using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Maze
{
    public class MazeBuilder
    {

        public Cell[,,] Maze { get; protected set; }
        private readonly int size;
        //private readonly int[] start = new int[] { 0, 0, 0 };
        private readonly int[,] radius = new int[,]
        {
          { 0,-1 },
          { -1, 0 },{ 1, 0 },
          { 0, 1 },
        };
        public MazeBuilder(int width, int height, int floors)
        {
        
            size = width * height * floors;
            Scaffold(width, height, floors);
            AssignNeighbors();
            Build();
            
        }

       
        private void Scaffold(int width, int height, int floors)
        {
            Console.WriteLine("Scaffolding");
            Maze = new Cell[floors, width, height];
            for (int z = 0; z < floors; z++)
            {
                for (int x = 0; x < width; x++)
                {

                    for (int y = 0; y < height; y++)
                    {
                        Maze[z, x, y] = new Cell
                        {
                            yAxis = y,
                            xAxis = x,
                            zAxis = z
                        };

                    }
                }
            }
            
            Console.WriteLine("Scaffolding Complete");
            
        }

      
        private void AssignNeighbors()
        {


            Console.WriteLine("Assigning Neighbors");
            foreach (Cell cell in Maze)
            {

                int z = cell.zAxis;
                //int y = cell.yAxis;
                //int x = cell.xAxis;
          

                for (int i = 0; i < radius.GetLength(0); i++)
                {
          
                    int targetX = radius[i, 0] + cell.xAxis;
                    int targetY = radius[i, 1] + cell.yAxis;

                    if (CheckBounds(z, targetX, targetY)== true)
                    {
                        cell.neighbors[i]= Maze[z, targetX, targetY];
                    }
                
                }

                if (CheckBounds(z - 1, cell.xAxis, cell.yAxis)== true)
                    {
                        cell.neighbors[4]=Maze[z-1, cell.xAxis, cell.yAxis];
               
                    }

                if (CheckBounds(z + 1, cell.xAxis, cell.yAxis)== true)
                    {
                      
                        cell.neighbors[5]=Maze[z+1, cell.xAxis, cell.yAxis];
                    }
            }
            Console.WriteLine("Assigning Neighbors");
                    
        }
        private bool CheckBounds(int z,int x,int y)
        {
            if (z < Maze.GetLength(0) &&
                z >= 0 &&
                x < Maze.GetLength(1) &&
                x >= 0 &&
                y < Maze.GetLength(2) &&
                y >= 0)
            { return true; }
            return false;
        }
        private void Build()
        {
            int count = 0;
            Random r = new Random();
            Cell current = Maze[0,0,0];
            while (count < size-1)
            {
                Cell[] allPaths = current.neighbors;
                var paths = current.neighbors.Where(cell => cell!= null && cell.visited==false);
                    //.Where(cell => cell.visited == false);
                int path;
                current.visited = true;
                if (current.origin != null) 
                {
                    DestroyWalls(current, current.origin);
                    foreach (bool wall in current.walls)
                    {
                    Console.WriteLine(wall.ToString());
                    }
                    
                }
                if (paths.Count() > 0)
                {
                    path = r.Next(0, (int)paths.Count());
                    // issue should we store null values
                    paths.ElementAt(path).origin = current;
                    current = paths.ElementAt(path);
                    count++;
                }
                else
                {
                    Console.WriteLine("Dead end... Backtracking");
                    current = current.origin;
                }
               // Console.WriteLine(count);
            }

        }
        private void DestroyWalls(Cell current,Cell origin ) 
        {
            origin.walls[Array.IndexOf(origin.neighbors, current)] = false;
            current.walls[Array.IndexOf(current.neighbors, origin)] = false;
        }
    

    }
}
