using System.Collections.Generic;

namespace FinixMakesGames.MazeGenerator.Core 
{
    /// <summary>
    /// A class that represents a Maze in raw data form
    /// </summary>
    public abstract class Grid
    {

        protected abstract void Initialize();

        protected abstract void AssignNeigbours();

        public abstract Cell RandomCell();

        public abstract Cell RandomCell(int links);

        /// <returns>The number of Cells within the Grid</returns>
        public abstract int Size();


        /// <returns>A list of Cells that have only one link to another cell</returns>
        public abstract List<Cell> DeadEnds();

        protected virtual string ContentsOfCell(Cell cell) 
        {
            return " ";
        }
    }
}

