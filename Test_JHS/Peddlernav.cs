using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Peddlernav : MonoBehaviour
{

    public Transform player;
    private NavMeshAgent nvAgent;
    public float followRange = 3.0f;  // 플레이어 추적 범위
    public float wanderRange = 10.0f; // 자율 이동 범위
    public float wanderInterval = 5.0f; // 자율 이동 간격
    private float timer;
    private float elapsedTime = 0.0f; // 경과 시간
    private float delayStartTime = 6.0f; // 13초 후에 작동 시작

    void Start()
    {
        nvAgent = GetComponent<NavMeshAgent>();
        timer = wanderInterval; // 타이머 초기화
    }

    void Update()
    { 
        // 13초가 지나야 아래 로직 실행
        elapsedTime += Time.deltaTime;
        if (elapsedTime < delayStartTime)
        {
            return; // 아직 시간이 지나지 않았으므로 Update 종료
        }
        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= followRange)
        {
            // 추적 범위 내일 경우 플레이어를 따라감
            nvAgent.SetDestination(player.position);
            timer = wanderInterval; // 타이머 초기화
        }
        else
        {
            // 자율 이동 타이머 증가
            timer += Time.deltaTime;

            // 자율 이동 간격에 도달하면 새로운 목적지 설정
            if (timer >= wanderInterval)
            {
                Vector3 randomPosition = GetRandomPointWithinBounds();
                nvAgent.SetDestination(randomPosition);
                timer = 0; // 타이머 초기화
            }
        }
    }

    // NavMesh 위의 유효한 랜덤 위치를 반환하는 메서드
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
