using UnityEngine;
using NavMeshPlus.Components;


public class NavMeshAwake : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;
    void Awake()
    {
        //navMeshSurface = GetComponent<NavMeshSurface>();
        //navMeshSurface.BuildNavMesh();
        gameObject.GetComponent<NavMeshSurface>().enabled = true;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
