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
        //이중 if 문을 통해 마우스 우클릭시면서 isSteamActive가 false일때  StartSteam 메서드 작동
        if (Input.GetMouseButton(1))
        {
            if (!isSteamActive)
            {
                StartSteam();
            }
        }
        //우클릭 상태가 아닐경우이면서 isSteamActive가 ture일때  StopSteam()메서드 작동
        else
        {
            if (isSteamActive)
            {
                StopSteam();
            }
        }
    }

    //현재 오브젝트의 position값을 월드 자표로 가져와서 스팀을 생성할 위치를 설정한다,(스팀 오브젝트를 y축 90도 회전을 해서 발생시킨다)
    //steamInstance는 스팀프리펩을 현재 오브젝트의 자식으로 넣고 위치값과 회전값을 저장하는 변수이다.
    //steamParticleSysten 변수는  파티클 시스템 컴포넌트플 steamInstance에 넣는다

    void StartSteam()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation * Quaternion.Euler(0, 90, 0); // Y축으로 90도 회전
        steamInstance = Instantiate(steamPrefab, position, rotation, transform);
        steamParticleSystem = steamInstance.GetComponent<ParticleSystem>();
        isSteamActive = true;

    }

    //steamParticleSystem이 null이 아닌경우에 작동하며 작동하게되면 steamParticleSystem을 중지시키고 일정시간이 지나면 파괴시킨다
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
