using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    private ParticleSystem fireParticleSystem;
    private bool isExtinguishing = false;
    private float extinguishDuration = 2f; // 소멸에 걸리는 시간

    void Start()
    {
        fireParticleSystem = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Steam") && !isExtinguishing)
        {
            StartCoroutine(ExtinguishFire());
        }
    }

    IEnumerator ExtinguishFire()
    {
        isExtinguishing = true;
        float startTime = Time.time;
        var mainModule = fireParticleSystem.main;
        float initialLifetime = mainModule.startLifetime.constant;

        while (Time.time < startTime + extinguishDuration)
        {
            float t = (Time.time - startTime) / extinguishDuration;
            mainModule.startLifetime = Mathf.Lerp(initialLifetime, 0, t);
            yield return null;
        }

        fireParticleSystem.Stop(); // 파티클 재생 일시 중지

        // 파티클이 모두 소멸될 때까지 기다립니다.
        yield return new WaitForSeconds(fireParticleSystem.main.startLifetime.constant);

        // 모든 파티클이 소멸되면 오브젝트를 삭제합니다.
        Destroy(gameObject);
    }
}
