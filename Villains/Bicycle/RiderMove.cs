using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RiderMove : MonoBehaviour
{
    public GameObject Subway_Floor; // 끝과 끝을 순찰할 오브젝트
    private NavMeshAgent nav;
    public float minX;
    public float maxX;
    public float minZ;  
    public float maxZ;
    private Transform tr;

    void Start()
    {
        GetXBounds();
        tr = GetComponent<Transform>();
        nav = GetComponent<NavMeshAgent>();
    }

    public void GetXBounds()
    {
        if (Subway_Floor.TryGetComponent<Renderer>(out Renderer renderer))
        {
            Bounds bounds = renderer.bounds; // Bounds를 통해 양 끝을 확인
            minX = bounds.min.x; // 클래스 멤버 변수에 저장
            maxX = bounds.max.x;
            minZ = bounds.min.z;
            maxZ = bounds.max.z;

            Debug.Log($"Min x: {minX}, Max x: {maxX}, Min z: {minZ}, Max z: {maxZ}");
        }
        else
        {
            Debug.LogError("Renderer가 없습니다. Bounds를 확인할 수 없습니다.");
        }
    }

    private void Update()
    {
        if(!nav.pathPending && nav.remainingDistance <= nav.stoppingDistance)
        {
            GoBycicleVillan();
        }
    }

    private void GoBycicleVillan()
    {
        float RandomX = UnityEngine.Random.Range( minX, maxX );
        float RandomZ = UnityEngine.Random.Range( minZ, maxZ );
        Vector3 RandomPosition = new Vector3( RandomX, tr.position.y,RandomZ);
        if (NavMesh.SamplePosition(RandomPosition, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            Debug.Log($"유효한 위치: {hit.position}");
            nav.SetDestination(hit.position);
        }
        else
        {
            Debug.LogWarning("유효하지 않은 랜덤 위치입니다. 다시 시도합니다.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                Debug.Log("플레이어와 충돌했습니다.");

                break;

            default:
                Debug.Log($"충돌: {collision.gameObject.tag}");
                break;
        }
    }

}
