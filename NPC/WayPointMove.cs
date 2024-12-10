using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMove : MonoBehaviour
{
    public GameObject Subway_Floor;
    private NavMeshAgent nvAgent;
    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;
    public float repeatInterval = 10f;
    private Transform tr;
    private bool isAgentActive = false;

    void Start()
    {
        nvAgent = GetComponent<NavMeshAgent>();
        nvAgent.enabled = false;
        Invoke("ActivateNavMeshAgent", 5f);
        GetXBounds();
        tr = GetComponent<Transform>();
    }
    public void GetXBounds()
    {
        if (Subway_Floor.TryGetComponent<Renderer>(out Renderer renderer))
        {
            Bounds bounds = renderer.bounds;
            minX = bounds.min.x;
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
        if (isAgentActive && !nvAgent.pathPending && nvAgent.remainingDistance <= nvAgent.stoppingDistance)
        {
            GoNextDestination();
        }
    }

    private void ActivateNavMeshAgent()
    {
        nvAgent.enabled = true; // NavMeshAgent Ȱ��ȭ
        isAgentActive = true; // Ȱ��ȭ ���� ����
        GoNextDestination(); // ù ������ ����
    }

    private void GoNextDestination()
    {
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomZ = UnityEngine.Random.Range(minZ, maxZ);
        Vector3 randomPosition = new Vector3(randomX, tr.position.y, randomZ);

        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            nvAgent.SetDestination(hit.position);
            Debug.Log($"�� ������: {hit.position}");
        }
        else
        {
            Debug.Log($"�� ������: ������ �ȵǾ����ϴ�.");
        }
    }

}
