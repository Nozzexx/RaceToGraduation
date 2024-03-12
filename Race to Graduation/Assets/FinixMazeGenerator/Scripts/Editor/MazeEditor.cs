#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FinixMakesGames.MazeGenerator.EditorScripts
{
    [CustomEditor(typeof(QuadMaze)), CanEditMultipleObjects]
    public class MazeEditor : Editor
    {
        [MenuItem("Tools/Maze Generator/Quad Maze", false, -1)]
        public static void AddMaze() 
        {
            GameObject gj = new GameObject();
            gj.name = "Quad Maze";

            //Add template prefabs
            QuadMaze maze = gj.AddComponent<QuadMaze>();
            maze.corridorObjects.Add(Resources.Load("Prefabs/Corridor") as GameObject);
            maze.cornerObjects.Add(Resources.Load("Prefabs/Corner") as GameObject);
            maze.crossroadObjects.Add(Resources.Load("Prefabs/Crossroads") as GameObject);
            maze.deadEndObjects.Add(Resources.Load("Prefabs/Deadend") as GameObject);
            maze.intersectionObjects.Add(Resources.Load("Prefabs/Intersection") as GameObject);

            maze.wallPrefab = Resources.Load("Prefabs/Wall") as GameObject;
            maze.pillarPrefab = Resources.Load("Prefabs/Pillar") as GameObject;

        }

        public override void OnInspectorGUI()
        {
            QuadMaze maze = (QuadMaze)target;

            DrawDefaultInspector();

            if (GUILayout.Button("Create Maze"))
            {
                maze.GenerateMaze();
            }
            if (GUILayout.Button("Destroy Maze"))
            {
                maze.ResetMaze();
            }
        }
    }
}

#endif