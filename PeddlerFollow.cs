using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

<<<<<<<< HEAD:Villains/Peddler/SellerFollow.cs
public class SellerFollow : MonoBehaviour
========
public class PeddlerFollow : MonoBehaviour
>>>>>>>> 4cffcb60416bf36c890e036ffac9c504beb5ecc6:PeddlerFollow.cs
{
    public NavMeshSurface navMeshSurface;  // �׺�޽��� ����� NavMeshSurface ������Ʈ

    // �� �Լ��� �ٸ� ��ũ��Ʈ���� ȣ��� �� ����
    public void UpdateNavMesh()
    {
        // NavMesh �ٽ� ����
        navMeshSurface.BuildNavMesh();
    }
}
