using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FinixMakesGames.MazeGenerator.Core;

namespace FinixMakesGames.MazeGenerator 
{
    public class UniqueTileModifier : MonoBehaviour, IMazePrefabModifier
    {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private int tileLinks = 1;

        public void ApplyModifier(QuadMaze maze)
        {
            List<Cell> candidates = new List<Cell>();
            foreach (Cell cell in maze.grid.cells)
            {
                if (cell.links.Length == tileLinks)
                {
                    candidates.Add(cell);
                }
            }

            foreach (Cell cell1 in candidates)
            {
                Cell candidate = candidates[Random.Range(0, candidates.Count)];

                if (maze.uniqueTiles.ContainsKey(candidate))
                {
                    candidates.Remove(candidate);
                    continue;
                }
                else
                {
                    maze.uniqueTiles.Add(candidate, tilePrefab);
                    return;
                }
            }
        }
    }
}

