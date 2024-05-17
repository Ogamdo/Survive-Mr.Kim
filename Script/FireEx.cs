using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEx : MonoBehaviour
{
    public GameObject steamPrefab; // ��ƼŬ ������
    private GameObject steamInstance; // ������ ��ƼŬ �ν��Ͻ�
    private ParticleSystem steamParticleSystem; // ��ƼŬ �ý���
    private bool isSteamActive = false; // ��ƼŬ Ȱ�� ����

    void Update()
    {
        // ���콺 ��Ŭ���� �����ϰ� �ִ� ���
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
        Quaternion rotation = transform.rotation * Quaternion.Euler(0, 90, 0); // Y������ 90�� ȸ��
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
