<<<<<<< HEAD:Villains/Peddler/PeddlernavMeshUpdate.cs
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class PeddlernavMeshUpdate : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;
    void Start()
    {
        InvokeRepeating("RebuildNavMesh", 0f, 1f); // 0초 후 시작해서 매 1초마다 실행
    }
    void RebuildNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class PeddlernavMeshUpdate : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;
    void Start()
    {
        InvokeRepeating("RebuildNavMesh", 0f, 1f); // 0초 후 시작해서 매 1초마다 실행
    }
    void RebuildNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }
}
>>>>>>> 4cffcb60416bf36c890e036ffac9c504beb5ecc6:Test_JHS/PeddlernavMeshUpdate.cs
