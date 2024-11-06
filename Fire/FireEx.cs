using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEx : MonoBehaviour
{
    public GameObject steamPrefab; // 파티클 프리팹
    private GameObject steamInstance; // 생성된 파티클 인스턴스
    private ParticleSystem steamParticleSystem; // 파티클 시스템
    public bool isSteamActive = false; // 파티클 활성 상태
    private bool isActive = false; // FireEx 활성 상태
    private int rightClickCount = 0; // 우클릭 횟수 카운트
    private int maxRightClicks = 15; // 최대 우클릭 횟수

    void Update()
    {
        if (!isActive) return; // 활성화되지 않은 경우 작동하지 않음

        // 마우스 우클릭을 한 번 감지
        if (Input.GetMouseButtonDown(1))
        {
            if (rightClickCount < maxRightClicks)
            {
                rightClickCount++; // 우클릭 횟수 증가
                Debug.Log("우클릭 횟수: " + rightClickCount);

                if (!isSteamActive && steamInstance == null) // 중복 생성 방지
                {
                    StartSteam();
                }
            }
        }

        // 마우스 우클릭을 뗐거나, 15번 이상 클릭하여 최대치에 도달한 경우 파티클을 멈춤
        if ((Input.GetMouseButtonUp(1) || rightClickCount > maxRightClicks) && isSteamActive)
        {
            StopSteam();
        }
    }
    public void StartSteam()
    {
        Vector3 position = transform.position + new Vector3(0, 1f, 0); // Y축으로 1 높게
        Quaternion rotation = transform.rotation * Quaternion.Euler(60, 90, 0); // 회전
        steamInstance = Instantiate(steamPrefab, position, rotation, transform);
        steamParticleSystem = steamInstance.GetComponent<ParticleSystem>();
        isSteamActive = true;

        // 파티클 생성 시 FireController 스크립트를 추가합니다.
        steamInstance.AddComponent<FireController>();
    }

    public void StopSteam()
    {
        if (steamParticleSystem != null)
        {
            steamParticleSystem.Stop();
            Destroy(steamInstance, steamParticleSystem.main.startLifetime.constantMax);
        }
        isSteamActive = false;
        steamInstance = null; // 파티클 인스턴스 초기화
    }


    public void Activate()
    {
        isActive = true;
    }
    public void Deativate()
    {
        isActive = false;
    }
}
