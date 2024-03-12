using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public NavMeshSurface surface;
    public bool updateCount = false;

    // Start is called before the first frame update
    void Start()
    {
        //surface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        if(updateCount == false)
        {
           surface.BuildNavMesh(); 
           updateCount = true;
        }
    }
}
