using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    private ParticleSystem fireParticleSystem;
    private bool isExtinguishing = false;
    private float extinguishDuration = 2f; // �Ҹ꿡 �ɸ��� �ð�

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

        fireParticleSystem.Stop(); // ��ƼŬ ��� �Ͻ� ����

        // ��ƼŬ�� ��� �Ҹ�� ������ ��ٸ��ϴ�.
        yield return new WaitForSeconds(fireParticleSystem.main.startLifetime.constant);

        // ��� ��ƼŬ�� �Ҹ�Ǹ� ������Ʈ�� �����մϴ�.
        Destroy(gameObject);
    }
}
