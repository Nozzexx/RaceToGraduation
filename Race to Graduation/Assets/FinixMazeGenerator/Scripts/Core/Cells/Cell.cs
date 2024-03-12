using System.Collections.Generic;

namespace FinixMakesGames.MazeGenerator.Core 
{
    /// <summary>
    /// The basic unit of a maze. Similar to a node in a graph
    /// </summary>
   public abstract class Cell
    {
        protected List<Cell> _links;
        /// <summary>
        /// Array of all Cells linked to this Cell
        /// </summary>
        public Cell[] links { get { return _links.ToArray(); } }

        /// <summary>
        /// Links this Cell to another Cell, making a path between them.
        /// </summary>
        /// <param name="bidirectional">Set if the link is both ways or just one way. Default set to true.</param>
        public void Link(Cell cell, bool bidirectional = true)
        {
            _links.Add(cell);

            if (bidirectional)
                cell.Link(this, false);
        }

        /// <summary>
        /// Removes a link from a choosen Cell to this Cell.
        /// </summary>
        /// <param name="bidirectional">Set if the link is to be removed both ways or just one way. Default set to true.</param>
        public void UnLink(Cell cell, bool bidirectional = true)
        {
            _links.Remove(cell);

            if (bidirectional)
                cell.UnLink(this, false);
        }

        public bool IsLinked(Cell cell)
        {
            return _links.Contains(cell);
        }


        /// <returns>A list of all neighboring cells whether they're linked or not.</returns>
        public abstract List<Cell> Neighbors();

        /// <returns>A Disances class with all paths and their relative Distance to this Cell.</returns>
        public Distances CellDistances() 
        {
            Distances distances = new Distances(this);
            List<Cell> frontier = new List<Cell>();
            frontier.Add(this);

            while (frontier.Count > 0)
            {
                List<Cell> newFrontier = new List<Cell>();

                foreach (Cell cell in frontier)
                {
                    foreach (Cell linked in cell.links)
                    {
                        if (distances[linked] == null)
                        {
                            distances[linked] = distances[cell] + 1;
                            newFrontier.Add(linked);
                        }
                    }
                }

                frontier = newFrontier;
            }

            return distances;
        }
    }
}

