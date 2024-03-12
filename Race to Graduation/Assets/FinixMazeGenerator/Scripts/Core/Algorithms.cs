using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FinixMakesGames.MazeGenerator.Core 
{
    public static class Algorithms
    {
        public static void BinaryTree(QuadGrid grid) 
        {

            foreach (QuadCell cell in grid.cells)
            {
                List<QuadCell> neighbors = new List<QuadCell>();

                if (cell.north != null)
                    neighbors.Add(cell.north);
                if (cell.east != null)
                    neighbors.Add(cell.east);

                if (neighbors.Count > 0)
                {
                    int index = Random.Range(0, neighbors.Count);
                    QuadCell neighbor = neighbors[index];

                    cell.Link(neighbor);
                }
            }
        }

        public static void Sidewinder(QuadGrid grid) 
        {

            for (int x = 0; x < grid.cells.GetLength(0); x++)
            {
                List<QuadCell> row = new List<QuadCell>();
                for (int y = 0; y < grid.cells.GetLength(1); y++)
                {
                    QuadCell cell = grid.cells[x, y];
                    row.Add(cell);

                    bool eastBounds = cell.east == null;
                    bool norhtBound = cell.north == null;

                    bool closeOut = eastBounds || (!norhtBound && Random.Range(0, 2) == 0);

                    if (closeOut)
                    {
                        QuadCell member = row[Random.Range(0, row.Count)];
                        if (member.north != null)
                            member.Link(member.north);
                        row.Clear();
                    }
                    else
                        cell.Link(cell.east);
                }
            }
        }

        public static void AldousBroder(Grid grid)
        {
            Cell current = grid.RandomCell();
            int unvisitedCells = grid.Size() - 1;

            while (unvisitedCells > 0)
            {
                List<Cell> neighbors = current.Neighbors();
                Cell candidate = neighbors[Random.Range(0, neighbors.Count)];

                if (candidate.links.Length == 0)
                {
                    current.Link(candidate);
                    unvisitedCells -= 1;
                }

                current = candidate;
            }
        }

        public static void Wilson(QuadGrid grid)
        {
            List<Cell> unvisited = new List<Cell>();
            foreach(Cell cell in grid.cells) unvisited.Add(cell);

            Cell first = unvisited[Random.Range(0, unvisited.Count)];
            unvisited.Remove(first);

            while (unvisited.Count > 0)
            {
                Cell cell = unvisited[Random.Range(0,unvisited.Count)];
                List<Cell> path = new List<Cell> { cell };

                while (unvisited.Contains(cell))
                {
                    var neighbors = cell.Neighbors();
                    cell = neighbors[Random.Range(0, neighbors.Count)];

                    var position = path.IndexOf(cell);

                    if (position != -1)
                        path.RemoveRange(position + 1, path.Count - position - 1);
                    else
                        path.Add(cell);
                }

                for (int i = 0; i < path.Count - 1; i++)
                {
                    path[i].Link(path[i + 1]);
                    unvisited.Remove(path[i]);
                }
            }
        }

        public static void HuntAndKill(QuadGrid grid)
        {
            Cell current = grid.RandomCell();

            while (current != null)
            {
                List<Cell> unvisitedNeighbors = current.Neighbors().FindAll(x => x.links.Length == 0);

                if (unvisitedNeighbors.Count > 0)
                {
                    Cell neighbor = unvisitedNeighbors[Random.Range(0, unvisitedNeighbors.Count)];
                    current.Link(neighbor);
                    current = neighbor;
                }
                else
                {
                    current = null;

                    foreach(Cell cell in grid.cells)
                    {
                        List<Cell> visitedNeighbors = cell.Neighbors().FindAll(x => x.links.Length > 0);
                        if (cell.links.Length == 0 && visitedNeighbors.Count > 0)
                        {
                            current = cell;
                            Cell neighbor = visitedNeighbors[Random.Range(0, visitedNeighbors.Count)];
                            current.Link(neighbor);
                            break;
                        }
                        
                    }
                }
            }
        }

        public static void RecursiveBacktracker(Grid grid)
        {
            Cell start = grid.RandomCell();
            Stack<Cell> stack = new Stack<Cell>();

            stack.Push(start);

            while (stack.Count > 0)
            {
                Cell current = stack.Peek();
                var neighbors = current.Neighbors().FindAll(x => x.links.Length == 0);

                if (neighbors.Count == 0)
                    stack.Pop();
                else 
                {
                    Cell neighbor = neighbors[Random.Range(0, neighbors.Count)];
                    current.Link(neighbor);
                    stack.Push(neighbor);
                }
            }
        }
    }
}

