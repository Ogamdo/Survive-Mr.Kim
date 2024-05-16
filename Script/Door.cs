using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject leftmovableObject;
    public GameObject rightmovableObject;

    public Vector3 rightmoveDirection = new Vector3(0, 0, -2);
    public Vector3 leftmoveDirection = new Vector3(0, 0, 2);
    public float moveDistance = 1.0f; 
    public float moveDuration = 1.0f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
        if (Input.GetMouseButtonDown(0))
        {
           
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            
            if (Physics.Raycast(ray, out hit))
            {
             
                if (hit.transform == transform)
                {
                 
                    StartCoroutine(MoveObject());
                }
            }
        }
    }

    IEnumerator MoveObject()
    {
        Vector3 startPosition = leftmovableObject.transform.position;
        Vector3 startPosition2 = rightmovableObject.transform.position;
        Vector3 endPosition = startPosition + (leftmoveDirection * moveDistance);
        Vector3 endPosition2 = startPosition2 + (rightmoveDirection * moveDistance);
        float elapsedTime = 0;
        float timeToReturn = 5f; // 이동 후 되돌아갈 시간

        while (elapsedTime < moveDuration)
        {
            // 이동
            leftmovableObject.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
            rightmovableObject.transform.position = Vector3.Lerp(startPosition2, endPosition2, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 이동 완료 후 대기
        yield return new WaitForSeconds(timeToReturn);

        // 되돌아가기
        float returnDuration = 1.0f; // 되돌아가는 시간
        float returnElapsedTime = 0;

        while (returnElapsedTime < returnDuration)
        {
            leftmovableObject.transform.position = Vector3.Lerp(endPosition, startPosition, returnElapsedTime / returnDuration);
            rightmovableObject.transform.position = Vector3.Lerp(endPosition2, startPosition2, returnElapsedTime / returnDuration);
            returnElapsedTime += Time.deltaTime;
            yield return null;
        }

        // 되돌아가기 완료 후 최종 위치 설정
        leftmovableObject.transform.position = startPosition;
        rightmovableObject.transform.position = startPosition2;
    }
}
