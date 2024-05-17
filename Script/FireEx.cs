using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEx : MonoBehaviour
{
    public GameObject steamPrefab; // 파티클 프리팹
    private GameObject steamInstance; // 생성된 파티클 인스턴스
    private ParticleSystem steamParticleSystem; // 파티클 시스템
    private bool isSteamActive = false; // 파티클 활성 상태

    void Update()
    {
        // 마우스 우클릭을 유지하고 있는 경우
        if (Input.GetMouseButton(1))
        {
            if (!isSteamActive)
            {
                StartSteam();
            }
        }
        else
        {
            if (isSteamActive)
            {
                StopSteam();
            }
        }
    }

    void StartSteam()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation * Quaternion.Euler(0, 90, 0); // Y축으로 90도 회전
        steamInstance = Instantiate(steamPrefab, position, rotation, transform);
        steamParticleSystem = steamInstance.GetComponent<ParticleSystem>();
        isSteamActive = true;

    }

    void StopSteam()
    {
        if (steamParticleSystem != null)
        {
            steamParticleSystem.Stop();
            Destroy(steamInstance, steamParticleSystem.main.startLifetime.constantMax);
        }
        isSteamActive = false;
    }
}
