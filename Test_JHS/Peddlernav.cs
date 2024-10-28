using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Peddlernav : MonoBehaviour
{

    public Transform player;
    private NavMeshAgent nvAgent;
    public float followRange = 3.0f;  // �÷��̾� ���� ����
    public float wanderRange = 10.0f; // ���� �̵� ����
    public float wanderInterval = 5.0f; // ���� �̵� ����
    private float timer;
    private float elapsedTime = 0.0f; // ��� �ð�
    private float delayStartTime = 6.0f; // 13�� �Ŀ� �۵� ����

    void Start()
    {
        nvAgent = GetComponent<NavMeshAgent>();
        timer = wanderInterval; // Ÿ�̸� �ʱ�ȭ
    }

    void Update()
    { 
        // 13�ʰ� ������ �Ʒ� ���� ����
        elapsedTime += Time.deltaTime;
        if (elapsedTime < delayStartTime)
        {
            return; // ���� �ð��� ������ �ʾ����Ƿ� Update ����
        }
        // �÷��̾���� �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= followRange)
        {
            // ���� ���� ���� ��� �÷��̾ ����
            nvAgent.SetDestination(player.position);
            timer = wanderInterval; // Ÿ�̸� �ʱ�ȭ
        }
        else
        {
            // ���� �̵� Ÿ�̸� ����
            timer += Time.deltaTime;

            // ���� �̵� ���ݿ� �����ϸ� ���ο� ������ ����
            if (timer >= wanderInterval)
            {
                Vector3 randomPosition = GetRandomPointWithinBounds();
                nvAgent.SetDestination(randomPosition);
                timer = 0; // Ÿ�̸� �ʱ�ȭ
            }
        }
    }

    // NavMesh ���� ��ȿ�� ���� ��ġ�� ��ȯ�ϴ� �޼���
    Vector3 GetRandomPointWithinBounds()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderRange, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return transform.position;
    }

}
