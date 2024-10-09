using System.Collections;
using UnityEngine;

public class ObjectChecker : MonoBehaviour
{
    public GameObject targetObject; // ������ ���� ������Ʈ
    private bool shouldCheck = false;

    void Start()
    {
        // ���� ���� �� 20�� �ڿ� �ڽ� ������Ʈ�� �����ϴ��� Ȯ�� ����
        StartCoroutine(InitialWait());
    }

    void Update()
    {
        if (shouldCheck && targetObject != null)
        {
            // �ڽ� ������Ʈ�� �������� ������ targetObject ����
            if (transform.childCount == 0)
            {
                Debug.Log("Target object has no children, deleting...");
                Destroy(targetObject);
                shouldCheck = false; // ������Ʈ�� �����Ǹ� �˻� ����
            }
            else
            {
                Debug.Log("Target object has children.");
            }
        }
    }

    IEnumerator InitialWait()
    {
        // 20�� ���
        yield return new WaitForSeconds(20f);
        shouldCheck = true; // 20�� �� �˻� ����
    }
}
