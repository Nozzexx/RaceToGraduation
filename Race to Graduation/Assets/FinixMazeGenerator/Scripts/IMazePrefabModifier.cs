using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FinixMakesGames.MazeGenerator.Core;

namespace FinixMakesGames.MazeGenerator
{
    public interface IMazePrefabModifier
    {
        public void ApplyModifier(QuadMaze maze);

    }
}

