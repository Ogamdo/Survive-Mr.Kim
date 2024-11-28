using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Peddlernav : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent nvAgent;
    public float followRange = 3.0f; // 추적 범위
    private bool navStopped = false;

    void Start()
    {
        nvAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (nvAgent == null || !nvAgent.enabled)
            return;

        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        // 거리가 추적 범위 이내일 경우 플레이어를 따라감
        if (distanceToPlayer <= followRange)
        {
            nvAgent.SetDestination(player.position);
            Debug.LogWarning("NavMeshAgent가 유효하지 않거나 NavMesh 위에 있지 않습니다.");
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
        Debug.Log("따라가기 멈춤");
        if (nvAgent != null)
        {
            nvAgent.enabled = false;  // NavMeshAgent 비활성화
            yield return new WaitForSeconds(2f);  // 2초 대기
            nvAgent.enabled = true;   // NavMeshAgent 활성화
        }
    }
}
