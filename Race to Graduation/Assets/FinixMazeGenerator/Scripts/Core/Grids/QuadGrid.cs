using System.Collections.Generic;
using UnityEngine;

namespace FinixMakesGames.MazeGenerator.Core 
{
    public class QuadGrid : Grid
    {
        private int _rows, _collumns;

        public QuadCell[,] cells;

        public Distances distances;

        public QuadGrid(int rows, int collumns)
        {
            _rows = rows;
            _collumns = collumns;

            cells = new QuadCell[rows, collumns];

            Initialize();
            AssignNeigbours();
        }

        protected override void Initialize()
        {
            for (int x = 0; x < _rows; x++)
            {
                for (int y = 0; y < _collumns; y++)
                {
                    cells[x, y] = new QuadCell(x, y);
                }
            }
        }

        protected override void AssignNeigbours()
        {
            for (int x = 0; x < _rows; x++)
            {
                for (int y = 0; y < _collumns; y++)
                {
                    cells[x, y].north = (x != 0) ? cells[x - 1, y] : null;
                    cells[x, y].south = (x != _rows - 1) ? cells[x + 1, y] : null;
                    cells[x, y].east = (y != _collumns - 1) ? cells[x, y + 1] : null;
                    cells[x, y].west = (y != 0) ? cells[x, y - 1] : null;
                }
            }
        }

        public override Cell RandomCell()
        {
           return cells[Random.Range(0, _rows), Random.Range(0, _collumns)];
        }

        public override Cell RandomCell(int links) 
        {
            List<Cell> contenders = new List<Cell>();

            foreach (Cell cell in cells)
                if (cell.links.Length == links)
                    contenders.Add(cell);

            return contenders[Random.Range(0, contenders.Count)];
        }

        public override int Size()
        {
            return _rows * _collumns;
        }

        public override string ToString()
        {
            var output = "+";
            for (int i = 0; i < _collumns; i++)
                output += "---+";
            output += $"\n";

            for (int x = 0; x < _rows; x++)
            {
                var top = "|";
                var bottom = "+";

                for (int y = 0; y < _collumns; y++)
                {
                    var body = " " + ContentsOfCell(cells[x,y]) + " ";
                    var east = cells[x, y].IsLinked(cells[x, y].east) ? " " : "|";

                    top += body + east;

                    var south = cells[x, y].IsLinked(cells[x, y].south) ? "   " : "---";
                    var corner = "+";

                    bottom += south + corner;
                }

                output += top + $"\n";
                output += bottom + $"\n";
            }

            return output;
        }

        public override List<Cell> DeadEnds()
        {
            List<Cell> deadEnds = new List<Cell>();

            foreach (Cell cell in cells)
            {
                if (cell.links.Length == 1)
                {
                    deadEnds.Add(cell);
                }
            }

            return deadEnds;
        }

        protected override string ContentsOfCell(Cell cell)
        {
            char ConvertToChar(int n)
            {
                char[] code = new char[] { 
                    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 
                    'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
                };

                return code[n % code.Length];
            }

            if (distances != null && distances[cell] != null)
                return "" + ConvertToChar((int)distances[cell]);
            else
                return base.ContentsOfCell(cell);
        }

        
    }
}


