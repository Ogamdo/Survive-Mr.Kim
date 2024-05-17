using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    //  private void OnParticleCollision(GameObject other)
    // {
    //if (other.gameObject.CompareTag("Fire"))
    //  {
    // "Enemy" �±׸� ������ �ִ� ������Ʈ�� ����
    //   Destroy(other.gameObject);
    // }

    //}
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Fire"))
        {
            // ������ �����ϴ� �ڷ�ƾ ����
            StartCoroutine(FadeAndDestroy(other));
        }
    }

    private IEnumerator FadeAndDestroy(GameObject obj)
    {
        // ������Ʈ�� Renderer�� �����ɴϴ�.
        // ������Ʈ�� Renderer�� �����ɴϴ�.
        Renderer renderer = obj.GetComponent<Renderer> ();
        if (renderer != null)
        {
            // ���� ������Ʈ�� ���� ������ �����ɴϴ�.
            Color color = renderer.material.color;

            // ������ ������ ���̴� �ݺ���
            for (float alpha = 1.0f; alpha > 0.0f; alpha -= 0.1f)
            {
                // ������ ���� ���� ������ ���ҽ�ŵ�ϴ�.
                color.a = alpha;
                renderer.material.color = color;

                // �� ������ ���
                yield return new WaitForSeconds(0.1f);
            }
        }

        // ������ 0�� �� �� ������Ʈ ����
        Destroy(obj);
    }
}
