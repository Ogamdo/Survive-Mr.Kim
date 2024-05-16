using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    private ParticleSystem fireParticleSystem;
    private float extinguishDelay = 3f; // 소멸 지연 시간

    void Start()
    {
        fireParticleSystem = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Steam"))
        {
            // 파티클이 닿은 후 파티클을 중지하고 지연 시간 후에 오브젝트를 삭제합니다.
            Invoke("ExtinguishAndDestroy", extinguishDelay);
        }
    }

    void ExtinguishAndDestroy()
    {
        // 파티클 재생 일시 중지
        fireParticleSystem.Stop();
        // 일정 시간 후에 오브젝트를 삭제합니다.
        Destroy(gameObject, extinguishDelay);
    }
}
