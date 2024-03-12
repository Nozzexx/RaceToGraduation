using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinixMakesGames.MazeGenerator.Core 
{
    public class QuadCell : Cell
    {
        public int row { get; private set; }
        public int collum { get; private set; }

        public QuadCell north, south, east, west;


        public QuadCell(int row, int collum)
        {
            this.row = row;
            this.collum = collum;

            north = null;
            south = null;
            west = null;
            east = null;

            _links = new List<Cell>();
        }

        public override List<Cell> Neighbors()
        {
            List<Cell> neighbors = new List<Cell>();

            if (north != null)
                neighbors.Add(north);
            if (south != null)
                neighbors.Add(south);
            if (west != null)
                neighbors.Add(west);
            if (east != null)
                neighbors.Add(east);

            return neighbors;
        }
    }
}

