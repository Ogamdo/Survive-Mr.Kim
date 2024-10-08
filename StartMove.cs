using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMove : MonoBehaviour
{
    public Transform Point1; // 시작 지점
    public Transform Point2; // 도착 지점
    public float moveSpeed = 5f; // 이동 속도 (단위: 유닛/초)
    private Animator anim; // Animator 변수 추가

    void Start()
    {
        // Animator 초기화
        anim = GetComponent<Animator>();

        // 게임 시작과 동시에 이동 코루틴 시작
        StartCoroutine(MoveObject(Point1.position, Point2.position, moveSpeed));
    }

    IEnumerator MoveObject(Vector3 startPoint, Vector3 endPoint, float speed)
    {
        float distance = Vector3.Distance(startPoint, endPoint);
        float duration = distance / speed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // 오브젝트 위치를 이동
            transform.position = Vector3.Lerp(startPoint, endPoint, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            anim.SetBool("Walk", true);
            yield return null;
        }

        // 최종 위치 설정
        transform.position = endPoint;
        
    }
}
