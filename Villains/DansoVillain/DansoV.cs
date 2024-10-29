using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DansoV : MonoBehaviour
{
    [SerializeField] float speedRot = 5;
    
    private NavMeshAgent nav;
    private float dis;
   private Transform tr; // 빌런의 포지션
 
    [SerializeField] GameObject danso; // 단소
    [SerializeField] GameObject player; // 플레이어 찾기
    // 물체를 발사할 방향
    [SerializeField] float speed = 10f; // 빌런의 이동속도
    [SerializeField] float range = 5.5f; // 플레이어에게 단소를 던지는 거리
    //GameManager 등에서 접근해야할 수도있으므로 public으로 선언
    public Coroutine tossCoroutine; // 코루틴 저장

    void Start()
    {
        tr = GetComponent<Transform>();
        nav = GetComponent<NavMeshAgent>();
        nav.speed = speed; // NavMeshAgent의 이동 속도 설정
    }

    void FixedUpdate()
    {
        dis= Vector3.Distance(player.transform.position, tr.position);
      if (dis < range)
        {
            if (tossCoroutine == null) // 코루틴이 실행 중이지 않다면
            {
                tossCoroutine = StartCoroutine(TossDansoCoroutine());
            }
        }
        else
        {
            if (tossCoroutine != null) // 코루틴이 실행 중이라면 중지
            {
                StopCoroutine(tossCoroutine);
                tossCoroutine = null;
            }
            FindPlayer();
        }
    }

    void FindPlayer()
    {
        nav.SetDestination(player.transform.position); // 플레이어의 위치로 목표 설정
      Vector3 direction = (player.transform.position - tr.position).normalized;
      Quaternion Rotation = Quaternion.LookRotation(direction);
      tr.rotation = Quaternion.Slerp(tr.rotation, Rotation, Time.deltaTime *speedRot);
       
    }
     IEnumerator TossDansoCoroutine()
    {
        while (true) // 무한 반복
        {
            tossDanso(); // 단소 던지기
            yield return new WaitForSeconds(1f); // 1초 대기
        }
    }

    void tossDanso()
    {
       Vector3 tossPosition = tr.position + tr.forward.normalized;
        GameObject flyingDanso = Instantiate(danso, tossPosition,tr.rotation);
       
    }
}
