using System.Collections;
using System.Collections.Generic;

namespace FinixMakesGames.MazeGenerator.Core
{
    /// <summary>
    /// A data collection of Maze Cells that forms a path or a set of paths relative to a root Cell and it's distance values
    /// </summary>
    public class Distances
    {
        private Cell _root;
        private Dictionary<Cell, int?> _cells;

        public Distances(Cell root)
        {
            _root = root;
            _cells = new Dictionary<Cell, int?>();
            _cells.Add(_root, 0);
        }

        public int? this [Cell cell] 
        {
            get { return _cells.ContainsKey(cell) ? _cells[cell] : null; }
            set { _cells[cell] = value; }
        }

        /// <returns>All cells in this Distance collection</returns>
        public IEnumerable<Cell> Cells() 
        {
            return _cells.Keys;
        }

        /// <summary>
        /// Calculates a path in a maze from the root Cell
        /// </summary>
        /// <param name="goal">Cell to reach</param>
        /// <returns>The collection of cells that make the path between the root Cell and the goal Cell</returns>
        public Distances PathTo(Cell goal) 
        {
            Cell current = goal;

            Distances breadcrumbs = new Distances(_root);
            breadcrumbs[current] = _cells[current];

            while (current != _root)
            {
                foreach (Cell neighbor in current.links)
                {
                    if (_cells[neighbor] < _cells[current])
                    {
                        breadcrumbs[neighbor] = _cells[neighbor];
                        current = neighbor;
                        break;
                    }
                }
            }

            return breadcrumbs;
        }

        public Cell Max()
        {
            int max_distance = 0;
            Cell max_cell = _root;

            foreach (Cell cell in Cells())
            {
                if (_cells[cell] > max_distance)
                {
                    max_cell = cell;
                    max_distance = (int)_cells[cell];
                }
            }

            return max_cell;
        }
    }
}

