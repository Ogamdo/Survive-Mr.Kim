using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Peddlernav : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent nvAgent;
    public float followRange = 3.0f; // ���� ����
    private bool navStopped = false;

    void Start()
    {
        nvAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (nvAgent == null || !nvAgent.enabled)
            return;

        // �÷��̾���� �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        // �Ÿ��� ���� ���� �̳��� ��� �÷��̾ ����
        if (distanceToPlayer <= followRange)
        {
            nvAgent.SetDestination(player.position);
            Debug.LogWarning("NavMeshAgent�� ��ȿ���� �ʰų� NavMesh ���� ���� �ʽ��ϴ�.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(NavmeshStop());
        }
    }

    private IEnumerator NavmeshStop()
    {
        Debug.Log("���󰡱� ����");
        if (nvAgent != null)
        {
            nvAgent.enabled = false;  // NavMeshAgent ��Ȱ��ȭ
            yield return new WaitForSeconds(2f);  // 2�� ���
            nvAgent.enabled = true;   // NavMeshAgent Ȱ��ȭ
        }
    }
}
