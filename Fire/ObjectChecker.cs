using System.Collections;
using UnityEngine;

public class ObjectChecker : MonoBehaviour
{
    public GameObject parentObject;
    public GameObject circle;
    private bool shouldCheck = false;

    void Start()
    {
        StartCoroutine(InitialWait());
    }

    void Update()
    {
        if (shouldCheck)
        {
            if (parentObject != null)
            {
                if (parentObject.transform.childCount == 0)
                {
                    Debug.Log("Parent object has no children.");

                    if (circle != null && !circle.activeSelf)
                    {
                        circle.SetActive(true);
                        Debug.Log($"Circle 활성화 완료: {circle.name} 위치 {circle.transform.position}");
                    }
                    shouldCheck = false;
                }
                else
                {
                    Debug.Log("Parent object still has children.");
                }
            }
            else
            {
                Debug.LogError("Parent object가 연결되지 않았습니다!");
                shouldCheck = false;
            }
        }
    }

    IEnumerator InitialWait()
    {
        yield return new WaitForSeconds(5f);
        shouldCheck = true;
    }
}
