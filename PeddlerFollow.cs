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
    public NavMeshSurface navMeshSurface;  // 네브메쉬가 적용된 NavMeshSurface 컴포넌트

    // 이 함수는 다른 스크립트에서 호출될 수 있음
    public void UpdateNavMesh()
    {
        // NavMesh 다시 빌드
        navMeshSurface.BuildNavMesh();
    }
}
