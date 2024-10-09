using System.Collections;
using UnityEngine;

public class ObjectChecker : MonoBehaviour
{
    public GameObject targetObject; // 삭제할 게임 오브젝트
    private bool shouldCheck = false;

    void Start()
    {
        // 게임 시작 후 20초 뒤에 자식 오브젝트가 존재하는지 확인 시작
        StartCoroutine(InitialWait());
    }

    void Update()
    {
        if (shouldCheck && targetObject != null)
        {
            // 자식 오브젝트가 존재하지 않으면 targetObject 삭제
            if (transform.childCount == 0)
            {
                Debug.Log("Target object has no children, deleting...");
                Destroy(targetObject);
                shouldCheck = false; // 오브젝트가 삭제되면 검사 중지
            }
            else
            {
                Debug.Log("Target object has children.");
            }
        }
    }

    IEnumerator InitialWait()
    {
        // 20초 대기
        yield return new WaitForSeconds(20f);
        shouldCheck = true; // 20초 후 검사 시작
    }
}
