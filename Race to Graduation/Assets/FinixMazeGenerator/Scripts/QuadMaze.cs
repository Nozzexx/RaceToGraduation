using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FinixMakesGames.MazeGenerator.Core;

namespace FinixMakesGames.MazeGenerator 
{
    public class QuadMaze : Maze
    {
        public string seed = "";
        [SerializeField] private float _cellSize = 10;

        [Space(10)]
        public int rows = 5;
        public int collumns = 5;

        [Space(10)]
        [SerializeField] private GenerationType _generationType = GenerationType.PrefabType;
        public GenerationType generationType { get { return _generationType; } }

        [SerializeField] private Algorithm _algorithm = Algorithm.RecursiveBacktracker;

        [Space(10)]
        public List<GameObject> corridorObjects = new List<GameObject>();
        public List<GameObject> cornerObjects = new List<GameObject>();
        public List<GameObject> intersectionObjects = new List<GameObject>();
        public List<GameObject> crossroadObjects = new List<GameObject>();
        public List<GameObject> deadEndObjects = new List<GameObject>();

        [Space(10)]
        public GameObject wallPrefab;
        public GameObject pillarPrefab;

        [Space(20)]
        public bool generateOnStart = true;
 
        private QuadGrid _grid;
        public QuadGrid grid
        {
            get { return _grid; }
        }

        //PrefabModifiers
        public Dictionary<Cell, GameObject> uniqueTiles = new Dictionary<Cell, GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            if (generateOnStart)
                GenerateMaze();
        }

        public void GenerateMaze()
        {
            if (transform.childCount != 0)
                ResetMaze();

            InstantiateMazeData();

            foreach (IMazePrefabModifier modifier in GetComponents<IMazePrefabModifier>())
                modifier.ApplyModifier(this);

            switch (_generationType)
            {
                case GenerationType.WallType:
                    InstantiateWallMaze();
                    break;
                case GenerationType.PrefabType:
                    InstantiatePrefabMaze();
                    break;
                default:
                    Debug.LogError("Unknown Generation Type");
                    break;
            }
        }

        private void InstantiateMazeData() 
        {
            _grid = new QuadGrid(rows, collumns);

            int hashSeed;
            if (seed.Length != 0 && seed != null)
                hashSeed = seed.GetHashCode();
            else
                hashSeed = System.DateTime.UtcNow.GetHashCode();

            Random.InitState(hashSeed);

            switch (_algorithm)
            {
                case Algorithm.BinaryTree:
                    Algorithms.BinaryTree(_grid);
                    break;
                case Algorithm.Sidewinder:
                    Algorithms.Sidewinder(_grid);
                    break;
                case Algorithm.AldousBroder:
                    Algorithms.AldousBroder(_grid);
                    break;
                case Algorithm.Wilson:
                    Algorithms.Wilson(_grid);
                    break;
                case Algorithm.HuntAndKill:
                    Algorithms.HuntAndKill(_grid);
                    break;
                case Algorithm.RecursiveBacktracker:
                    Algorithms.RecursiveBacktracker(_grid);
                    break;
                default:
                    Debug.LogError("Uknown Algorithm");
                    break;
            }
        }
        
        private bool IsUnique(QuadCell cell) 
        {
            return uniqueTiles.ContainsKey(cell);
        }

        private void InstantiatePrefabMaze() 
        {
            //Check if prefeab lists are empty
            if (corridorObjects.Count == 0 || cornerObjects.Count == 0 || intersectionObjects.Count == 0 || crossroadObjects.Count == 0 || deadEndObjects.Count == 0)
            {
                Debug.LogError("Empty prefab list, please insert the appropriate prefabs");
                return;
            }

            float width = _grid.cells.GetLength(0) * _cellSize;
            float height = _grid.cells.GetLength(1) * _cellSize;

            foreach (QuadCell cell in _grid.cells)
            {
                Vector3 spawnPosition = new Vector3((cell.collum + 0.5f) * _cellSize - height / 2f, 0, width / 2f - (cell.row + 0.5f) * _cellSize);
                GameObject cellPF = null;

                //Instatiate and rotate acording to every possible scenario
                //Also check if the Tile is marked as a modified Tile, then instatiate that special prefab
                //Not very clean code but it works...
                switch (cell.links.Length)
                {
                    case 1: //If is a Dead End

                        if (cell.links[0] == cell.north)
                        {
                            cellPF = Instantiate(IsUnique(cell) ? uniqueTiles[cell] : deadEndObjects[Random.Range(0, deadEndObjects.Count)], this.transform);
                            cellPF.transform.localPosition = spawnPosition;
                        }
                        else if (cell.links[0] == cell.south)
                        {
                            cellPF = Instantiate(IsUnique(cell) ? uniqueTiles[cell] : deadEndObjects[Random.Range(0, deadEndObjects.Count)], this.transform);
                            cellPF.transform.localPosition = spawnPosition;
                            cellPF.transform.Rotate(new Vector3(0, 180, 0));
                        }
                        else if (cell.links[0] == cell.west)
                        {
                            cellPF = Instantiate(IsUnique(cell) ? uniqueTiles[cell] : deadEndObjects[Random.Range(0, deadEndObjects.Count)], this.transform);
                            cellPF.transform.localPosition = spawnPosition;
                            cellPF.transform.Rotate(new Vector3(0, 270, 0));

                        }
                        else if (cell.links[0] == cell.east)
                        {
                            cellPF = Instantiate(IsUnique(cell) ? uniqueTiles[cell] : deadEndObjects[Random.Range(0, deadEndObjects.Count)], this.transform);
                            cellPF.transform.localPosition = spawnPosition;
                            cellPF.transform.Rotate(new Vector3(0, 90, 0));
                        }

                        break;
                    case 2: //If is a corner or a corridor

                        if (cell.links.Contains(cell.north) && cell.links.Contains(cell.east))
                        {
                            cellPF = Instantiate(IsUnique(cell) ? uniqueTiles[cell] : cornerObjects[Random.Range(0, cornerObjects.Count)], this.transform);
                            cellPF.transform.localPosition = spawnPosition;
                        }
                        else if (cell.links.Contains(cell.east) && cell.links.Contains(cell.south))
                        {
                            cellPF = Instantiate(IsUnique(cell) ? uniqueTiles[cell] : cornerObjects[Random.Range(0, cornerObjects.Count)], this.transform);
                            cellPF.transform.localPosition = spawnPosition;
                            cellPF.transform.Rotate(new Vector3(0, 90, 0));
                        }
                        else if (cell.links.Contains(cell.south) && cell.links.Contains(cell.west))
                        {
                            cellPF = Instantiate(IsUnique(cell) ? uniqueTiles[cell] : cornerObjects[Random.Range(0, cornerObjects.Count)], this.transform);
                            cellPF.transform.localPosition = spawnPosition;
                            cellPF.transform.Rotate(new Vector3(0, 180, 0));
                        }
                        else if (cell.links.Contains(cell.west) && cell.links.Contains(cell.north))
                        {
                            cellPF = Instantiate(IsUnique(cell) ? uniqueTiles[cell] : cornerObjects[Random.Range(0, cornerObjects.Count)], this.transform);
                            cellPF.transform.localPosition = spawnPosition;
                            cellPF.transform.Rotate(new Vector3(0, 270, 0));
                        }
                        else if (cell.links.Contains(cell.north) && cell.links.Contains(cell.south))
                        {
                            cellPF = Instantiate(IsUnique(cell) ? uniqueTiles[cell] : corridorObjects[Random.Range(0, corridorObjects.Count)], this.transform);
                            cellPF.transform.localPosition = spawnPosition;
                        }
                        else if (cell.links.Contains(cell.east) && cell.links.Contains(cell.west))
                        {
                            cellPF = Instantiate(IsUnique(cell) ? uniqueTiles[cell] : corridorObjects[Random.Range(0, corridorObjects.Count)], this.transform);
                            cellPF.transform.localPosition = spawnPosition;
                            cellPF.transform.Rotate(new Vector3(0, 90, 0));
                        }
                            break;
                    case 3: // If is an Intersection

                        if (cell.links.Contains(cell.north) && cell.links.Contains(cell.east) && cell.links.Contains(cell.south))
                        {
                            cellPF = Instantiate(IsUnique(cell) ? uniqueTiles[cell] : intersectionObjects[Random.Range(0, intersectionObjects.Count)], this.transform);
                            cellPF.transform.localPosition = spawnPosition;
                        }
                        else if (cell.links.Contains(cell.east) && cell.links.Contains(cell.south) && cell.links.Contains(cell.west))
                        {
                            cellPF = Instantiate(IsUnique(cell) ? uniqueTiles[cell] : intersectionObjects[Random.Range(0, intersectionObjects.Count)], this.transform);
                            cellPF.transform.localPosition = spawnPosition;
                            cellPF.transform.Rotate(new Vector3(0, 90, 0));
                        }
                        else if (cell.links.Contains(cell.south) && cell.links.Contains(cell.west) && cell.links.Contains(cell.north))
                        {
                            cellPF = Instantiate(IsUnique(cell) ? uniqueTiles[cell] : intersectionObjects[Random.Range(0, intersectionObjects.Count)], this.transform);
                            cellPF.transform.localPosition = spawnPosition;
                            cellPF.transform.Rotate(new Vector3(0, 180, 0));
                        }
                        else if (cell.links.Contains(cell.west) && cell.links.Contains(cell.north) && cell.links.Contains(cell.east))
                        {
                            cellPF = Instantiate(IsUnique(cell) ? uniqueTiles[cell] : intersectionObjects[Random.Range(0, intersectionObjects.Count)], this.transform);
                            cellPF.transform.localPosition = spawnPosition;
                            cellPF.transform.Rotate(new Vector3(0, 270, 0));
                        }

                        break;
                    case 4: //If is a crossroad
                        {
                            cellPF = Instantiate(IsUnique(cell) ? uniqueTiles[cell] : crossroadObjects[Random.Range(0, crossroadObjects.Count)], this.transform);
                            cellPF.transform.localPosition = spawnPosition;
                            cellPF.transform.Rotate(new Vector3(0, 90 * Random.Range(0, 3), 0));
                        }
                        break;
                    default :
                        Debug.LogError("Out of range links, What happen?");
                        break;
                }
            }
        }

        private void InstantiateWallMaze() 
        {
            if (_grid == null)
                InstantiateMazeData();

            float width = _grid.cells.GetLength(0) * _cellSize;
            float height = _grid.cells.GetLength(1) * _cellSize;

            var pillar = Instantiate(pillarPrefab, this.transform);
            var pillar0 = Instantiate(pillarPrefab, this.transform);
            pillar.transform.localPosition = new Vector3(height / 2, 0, -width /2);
            pillar0.transform.localPosition = new Vector3(-height / 2, 0, width / 2);

            foreach (QuadCell cell in _grid.cells)
            {
                //Pillar Instatiation

                var pillar1 = Instantiate(pillarPrefab, this.transform);
                var pillar2 = Instantiate(pillarPrefab, this.transform);

                var y1 = cell.collum * _cellSize - height/2;
                var x1 = cell.row * _cellSize - width / 2;
                var y2 = (cell.collum + 1) * _cellSize - height / 2;
                var x2 = (cell.row + 1) * _cellSize - width / 2;

                pillar1.transform.localPosition = new Vector3(x1, 0, y1);
                pillar2.transform.localPosition = new Vector3(x2, 0, y2);

                //Wall Instatiation

                if (cell.north == null)
                {
                    var wall = Instantiate(wallPrefab, this.transform);
                    wall.transform.localPosition = new Vector3(x1, 0, y2 - _cellSize / 2);
                    wall.transform.localScale = new Vector3(wall.transform.localScale.x,wall.transform.localScale.y, _cellSize);
                }

                if (cell.west == null)
                {
                    var wall = Instantiate(wallPrefab, this.transform);
                    wall.transform.localPosition = new Vector3(x1 + _cellSize / 2, 0, y1);
                    wall.transform.Rotate(new Vector3(0, 90, 0));
                    wall.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y, _cellSize);
                }

                if (!cell.IsLinked(cell.east))
                {
                    var wall = Instantiate(wallPrefab, this.transform);
                    wall.transform.localPosition = new Vector3(x1 + _cellSize / 2, 0, y2);
                    wall.transform.Rotate(new Vector3(0, 90, 0));
                    wall.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y, _cellSize);
                }

                if (!cell.IsLinked(cell.south)) 
                {
                    var wall = Instantiate(wallPrefab, this.transform);
                    wall.transform.localPosition = new Vector3(x2, 0, y2 - _cellSize / 2);
                    wall.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y, _cellSize);
                }
               

            }
        }

        public void ResetMaze() 
        {
            for (int i = this.transform.childCount; i > 0; --i)
                DestroyImmediate(this.transform.GetChild(0).gameObject);
        }

    }
}

