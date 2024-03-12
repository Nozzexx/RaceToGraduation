using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FinixMakesGames.MazeGenerator.Core;

namespace FinixMakesGames.MazeGenerator
{
    public abstract class Maze : MonoBehaviour
    {
        public enum GenerationType { WallType, PrefabType }
        public enum Algorithm { BinaryTree, Sidewinder, AldousBroder, Wilson, HuntAndKill, RecursiveBacktracker }
    }
}

