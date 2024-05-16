using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    private ParticleSystem fireParticleSystem;
    private float extinguishDelay = 3f; // �Ҹ� ���� �ð�

    void Start()
    {
        fireParticleSystem = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Steam"))
        {
            // ��ƼŬ�� ���� �� ��ƼŬ�� �����ϰ� ���� �ð� �Ŀ� ������Ʈ�� �����մϴ�.
            Invoke("ExtinguishAndDestroy", extinguishDelay);
        }
    }

    void ExtinguishAndDestroy()
    {
        // ��ƼŬ ��� �Ͻ� ����
        fireParticleSystem.Stop();
        // ���� �ð� �Ŀ� ������Ʈ�� �����մϴ�.
        Destroy(gameObject, extinguishDelay);
    }
}
