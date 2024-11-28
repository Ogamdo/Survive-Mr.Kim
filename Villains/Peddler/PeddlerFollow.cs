using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class PeddlerFollow : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;  // 네브메쉬가 적용된 NavMeshSurface 컴포넌트

    public void UpdateNavMesh()
    {
        // NavMesh 다시 빌드
        navMeshSurface.BuildNavMesh();
    }
}
