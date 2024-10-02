using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    //Fire�̶�� �ױ׸� ���� ��ü�� ��ƼŬ�� �浹������ FadeAndDestroy �ڷ�ƾ�� �̿��ؼ� �񵿱������� ����Ǵ� �۾��� �����Ѵ�.
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Fire"))
        {
            StartCoroutine(FadeAndDestroy(other));
        }
    }

    //obj������Ʈ�� Render������Ʈ�� �÷����� �������� for�ݺ����� ���ؼ� alpha���� �ٿ����鼭 ������Ʈ�� �����ϰ� �����
    //������ �ٿ����� �ݺ����� alpha���� 0 ���� ����� obj������Ʈ�� �ı��Ѵ�.
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
