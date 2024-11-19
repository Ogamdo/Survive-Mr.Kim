using UnityEngine;
using System.Collections;

public class BearV : MonoBehaviour
{
    public float hugDuration = 2.0f;          // 껴안기 지속 시간
    public int requiredPressCount = 10;       // 껴안기에서 벗어나기 위한 스페이스바 누름 횟수

    private bool isHugging = false;           // 껴안기 상태 확인
    private GameObject player;                // 플레이어 오브젝트 참조
    private float hugTimer;                   // 껴안기 타이머
    private int spacePressCount;              // 스페이스바 누름 횟수

    private float originalSpeed;              // 원래 플레이어 속도
    private CharMove playerMovement;          // 플레이어 이동 스크립트 참조

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isHugging)
        {
            player = other.gameObject;
            playerMovement = player.GetComponent<CharMove>();
            if (playerMovement != null)
            {
                originalSpeed = playerMovement.speed; // 원래 속도 저장
                StartCoroutine(HugPlayer());          // 껴안기 시작
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            StopCoroutine(HugPlayer());
            EndHug();
        }
    }

    IEnumerator HugPlayer()
    {
        isHugging = true;
        hugTimer = hugDuration;
        spacePressCount = 0;

        // 플레이어 속도 0으로 설정하여 움직임 멈춤
        playerMovement.SetSpeed(0);

        while (hugTimer > 0 && spacePressCount < requiredPressCount)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                spacePressCount++;
                // UI 업데이트 기능이 있다면 여기에 추가
            }

            hugTimer -= Time.deltaTime;
            yield return null;
        }

        EndHug();
    }

    void EndHug()
    {
        if (playerMovement != null)
        {
            playerMovement.SetSpeed(originalSpeed); // 원래 속도로 복원
        }
        isHugging = false;
    }
}
