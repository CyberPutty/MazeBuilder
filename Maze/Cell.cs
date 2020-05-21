using System;
using System.Collections.Generic;
using System.Text;

namespace Maze
{
    public class Cell
    {

        public int zAxis { get; set; }
        public int yAxis { get; set; }
        public int xAxis { get; set; }
        public Cell[] neighbors= new Cell[6];
        public bool visited = false;
        public bool[] walls = new bool[]{true,true,true,true,true,true};//clockwise n/e/s/w/up/down
    
        public Cell origin;

    }
}
