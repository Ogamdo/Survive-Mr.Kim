using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class PeddlerFollow : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;  // �׺�޽��� ����� NavMeshSurface ������Ʈ

    public void UpdateNavMesh()
    {
        // NavMesh �ٽ� ����
        navMeshSurface.BuildNavMesh();
    }
}
