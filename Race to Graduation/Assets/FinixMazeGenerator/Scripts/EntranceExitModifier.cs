using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FinixMakesGames.MazeGenerator.Core;

namespace FinixMakesGames.MazeGenerator 
{
    public class EntranceExitModifier : MonoBehaviour, IMazePrefabModifier
    {
        Cell entraceCell;
        Cell exitCell;

        [SerializeField] private GameObject entrancePrefab;
        [SerializeField] private int entranceLinks = 1;

        [SerializeField] private GameObject exitPrefab;
        [SerializeField] private int exitLinks = 1;

        private void GetMazeData(QuadGrid grid) 
        {
            List<Cell> entryCells = new List<Cell>();
            List<Cell> exitCells = new List<Cell>();

            //Store all candidate Cells in the apropriate lists
            foreach (QuadCell cell in grid.cells)
            {
                if (cell.links.Length == entranceLinks)
                    entryCells.Add(cell);
                if (cell.links.Length == exitLinks)
                    exitCells.Add(cell);
            }

            int best = 0;

            //Find the pair of Cells that are furthest apart
            foreach (Cell entrance in entryCells)
            {   
                foreach (Cell exit in exitCells)
                {
                    grid.distances = entrance.CellDistances().PathTo(exit);
                    if ((int)grid.distances[exit] > best)
                    {
                        best = (int)grid.distances[exit];
                        entraceCell = entrance;
                        exitCell = exit;
                    }
                }
            }

            //Set maze distances data to the entrance and exit path
            grid.distances = entraceCell.CellDistances().PathTo(exitCell);
        }

        public void ApplyModifier(QuadMaze maze)
        {
            GetMazeData(maze.grid);

            maze.uniqueTiles.Add(entraceCell, entrancePrefab);
            maze.uniqueTiles.Add(exitCell, exitPrefab);
        }        
    }
}


