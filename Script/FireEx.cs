using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEx : MonoBehaviour
{
    public GameObject steamPrefab; 
    private GameObject steamInstance;
    private ParticleSystem steamParticleSystem; 
    private bool isSteamActive = false; 

    void Update()
    {
        //���� if ���� ���� ���콺 ��Ŭ���ø鼭 isSteamActive�� false�϶�  StartSteam �޼��� �۵�
        if (Input.GetMouseButton(1))
        {
            if (!isSteamActive)
            {
                StartSteam();
            }
        }
        //��Ŭ�� ���°� �ƴҰ���̸鼭 isSteamActive�� ture�϶�  StopSteam()�޼��� �۵�
        else
        {
            if (isSteamActive)
            {
                StopSteam();
            }
        }
    }

    //���� ������Ʈ�� position���� ���� ��ǥ�� �����ͼ� ������ ������ ��ġ�� �����Ѵ�,(���� ������Ʈ�� y�� 90�� ȸ���� �ؼ� �߻���Ų��)
    //steamInstance�� ������������ ���� ������Ʈ�� �ڽ����� �ְ� ��ġ���� ȸ������ �����ϴ� �����̴�.
    //steamParticleSysten ������  ��ƼŬ �ý��� ������Ʈ�� steamInstance�� �ִ´�

    void StartSteam()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation * Quaternion.Euler(0, 90, 0); // Y������ 90�� ȸ��
        steamInstance = Instantiate(steamPrefab, position, rotation, transform);
        steamParticleSystem = steamInstance.GetComponent<ParticleSystem>();
        isSteamActive = true;

    }

    //steamParticleSystem�� null�� �ƴѰ�쿡 �۵��ϸ� �۵��ϰԵǸ� steamParticleSystem�� ������Ű�� �����ð��� ������ �ı���Ų��
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
