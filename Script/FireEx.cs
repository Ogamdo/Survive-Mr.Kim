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
