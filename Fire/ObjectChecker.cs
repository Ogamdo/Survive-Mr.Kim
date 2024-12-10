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
                        Debug.Log($"Circle Ȱ��ȭ �Ϸ�: {circle.name} ��ġ {circle.transform.position}");
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
                Debug.LogError("Parent object�� ������� �ʾҽ��ϴ�!");
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
