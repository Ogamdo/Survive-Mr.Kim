using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RiderMove : MonoBehaviour
{
    public GameObject Subway_Floor; // ���� ���� ������ ������Ʈ
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
            Bounds bounds = renderer.bounds; // Bounds�� ���� �� ���� Ȯ��
            minX = bounds.min.x; // Ŭ���� ��� ������ ����
            maxX = bounds.max.x;
            minZ = bounds.min.z;
            maxZ = bounds.max.z;

            Debug.Log($"Min x: {minX}, Max x: {maxX}, Min z: {minZ}, Max z: {maxZ}");
        }
        else
        {
            Debug.LogError("Renderer�� �����ϴ�. Bounds�� Ȯ���� �� �����ϴ�.");
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
            Debug.Log($"��ȿ�� ��ġ: {hit.position}");
            nav.SetDestination(hit.position);
        }
        else
        {
            Debug.LogWarning("��ȿ���� ���� ���� ��ġ�Դϴ�. �ٽ� �õ��մϴ�.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                Debug.Log("�÷��̾�� �浹�߽��ϴ�.");

                break;

            default:
                Debug.Log($"�浹: {collision.gameObject.tag}");
                break;
        }
    }

}
