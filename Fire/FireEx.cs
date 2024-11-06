using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEx : MonoBehaviour
{
    public GameObject steamPrefab; // ��ƼŬ ������
    private GameObject steamInstance; // ������ ��ƼŬ �ν��Ͻ�
    private ParticleSystem steamParticleSystem; // ��ƼŬ �ý���
    public bool isSteamActive = false; // ��ƼŬ Ȱ�� ����
    private bool isActive = false; // FireEx Ȱ�� ����
    private int rightClickCount = 0; // ��Ŭ�� Ƚ�� ī��Ʈ
    private int maxRightClicks = 15; // �ִ� ��Ŭ�� Ƚ��

    void Update()
    {
        if (!isActive) return; // Ȱ��ȭ���� ���� ��� �۵����� ����

        // ���콺 ��Ŭ���� �� �� ����
        if (Input.GetMouseButtonDown(1))
        {
            if (rightClickCount < maxRightClicks)
            {
                rightClickCount++; // ��Ŭ�� Ƚ�� ����
                Debug.Log("��Ŭ�� Ƚ��: " + rightClickCount);

                if (!isSteamActive && steamInstance == null) // �ߺ� ���� ����
                {
                    StartSteam();
                }
            }
        }

        // ���콺 ��Ŭ���� �ðų�, 15�� �̻� Ŭ���Ͽ� �ִ�ġ�� ������ ��� ��ƼŬ�� ����
        if ((Input.GetMouseButtonUp(1) || rightClickCount > maxRightClicks) && isSteamActive)
        {
            StopSteam();
        }
    }
    public void StartSteam()
    {
        Vector3 position = transform.position + new Vector3(0, 1f, 0); // Y������ 1 ����
        Quaternion rotation = transform.rotation * Quaternion.Euler(60, 90, 0); // ȸ��
        steamInstance = Instantiate(steamPrefab, position, rotation, transform);
        steamParticleSystem = steamInstance.GetComponent<ParticleSystem>();
        isSteamActive = true;

        // ��ƼŬ ���� �� FireController ��ũ��Ʈ�� �߰��մϴ�.
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
        steamInstance = null; // ��ƼŬ �ν��Ͻ� �ʱ�ȭ
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
