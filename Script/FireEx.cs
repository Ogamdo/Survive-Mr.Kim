using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEx : MonoBehaviour
{
    public GameObject steamPrefab; // 파티클 프리팹
    private GameObject steamInstance; // 생성된 파티클 인스턴스
    private ParticleSystem steamParticleSystem; // 파티클 시스템
    private bool isSteamActive = false; // 파티클 활성 상태
    public Transform playerTransform; // 플레이어 Transform을 참조하기 위한 변수

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
        Quaternion rotation = playerTransform.rotation; // 플레이어의 현재 회전 값

        steamInstance = Instantiate(steamPrefab, position, rotation, transform);
        steamParticleSystem = steamInstance.GetComponent<ParticleSystem>();
        isSteamActive = true;

        // 파티클 생성시 FireController 스크립트를 추가합니다.
        steamInstance.AddComponent<FireController>();
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

    void OnCollisionEnter(Collision collision)
    {
        // Player 태그를 가진 게임오브젝트와 충돌한 경우
        if (collision.gameObject.CompareTag("Player"))
        {
            // FireEx 오브젝트를 Player 오브젝트의 자식으로 설정
            transform.SetParent(collision.transform);
        }
    }
}