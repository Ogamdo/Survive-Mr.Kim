using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    //  private void OnParticleCollision(GameObject other)
    // {
    //if (other.gameObject.CompareTag("Fire"))
    //  {
    // "Enemy" 태그를 가지고 있는 오브젝트만 삭제
    //   Destroy(other.gameObject);
    // }

    //}
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Fire"))
        {
            // 서서히 삭제하는 코루틴 시작
            StartCoroutine(FadeAndDestroy(other));
        }
    }

    private IEnumerator FadeAndDestroy(GameObject obj)
    {
        // 오브젝트의 Renderer를 가져옵니다.
        // 오브젝트의 Renderer를 가져옵니다.
        Renderer renderer = obj.GetComponent<Renderer> ();
        if (renderer != null)
        {
            // 현재 오브젝트의 색상 정보를 가져옵니다.
            Color color = renderer.material.color;

            // 서서히 투명도를 줄이는 반복문
            for (float alpha = 1.0f; alpha > 0.0f; alpha -= 0.1f)
            {
                // 색상의 알파 값을 서서히 감소시킵니다.
                color.a = alpha;
                renderer.material.color = color;

                // 한 프레임 대기
                yield return new WaitForSeconds(0.1f);
            }
        }

        // 투명도가 0이 된 후 오브젝트 삭제
        Destroy(obj);
    }
}
