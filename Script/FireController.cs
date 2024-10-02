using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    //Fire이라는 테그를 가진 물체와 파티클이 충돌했을때 FadeAndDestroy 코루틴을 이용해서 비동기적으로 실행되는 작업을 실행한다.
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Fire"))
        {
            StartCoroutine(FadeAndDestroy(other));
        }
    }

    //obj오브젝트의 Render컴포너트와 컬러값을 가져오고 for반복문을 통해서 alpha값을 줄여가면서 오브젝트를 투명하게 만들고
    //서서히 줄여가는 반복문이 alpha값을 0 으로 만들면 obj오브젝트를 파괴한다.
    private IEnumerator FadeAndDestroy(GameObject obj)
    {
       
        Renderer renderer = obj.GetComponent<Renderer> ();
        if (renderer != null)
        {
      
            Color color = renderer.material.color;

      
            for (float alpha = 1.0f; alpha > 0.0f; alpha -= 0.1f)
            {
            
                color.a = alpha;
                renderer.material.color = color;

                yield return new WaitForSeconds(0.1f);
            }
        }


        Destroy(obj);
    }
}
