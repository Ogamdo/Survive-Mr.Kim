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
    // 유니티의 Raycast 시스템을 이용해서 화면내에서 특정 오브젝트를 좌클릭시에 MoveObject()메서드를 발동시켜서 문의 transform을 움직이게한다. 
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

    //MoveObject는 왼쪽 오른쪽의 움직임을 담당하게 된다. 소요시간을 설정함으로써 문이 열리는 소요시간과 다시 닫히는 시간을 정해둔다.
    IEnumerator MoveObject()
    {
        Vector3 startPosition = leftmovableObject.transform.position;
        Vector3 startPosition2 = rightmovableObject.transform.position;
        Vector3 endPosition = startPosition + (leftmoveDirection * moveDistance);
        Vector3 endPosition2 = startPosition2 + (rightmoveDirection * moveDistance);
        float elapsedTime = 0;
        float timeToReturn = 5f; 

        while (elapsedTime < moveDuration)
        {
           
            leftmovableObject.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
            rightmovableObject.transform.position = Vector3.Lerp(startPosition2, endPosition2, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

      
        yield return new WaitForSeconds(timeToReturn);

    
        float returnDuration = 1.0f; 
        float returnElapsedTime = 0;

        while (returnElapsedTime < returnDuration)
        {
            leftmovableObject.transform.position = Vector3.Lerp(endPosition, startPosition, returnElapsedTime / returnDuration);
            rightmovableObject.transform.position = Vector3.Lerp(endPosition2, startPosition2, returnElapsedTime / returnDuration);
            returnElapsedTime += Time.deltaTime;
            yield return null;
        }

        
        leftmovableObject.transform.position = startPosition;
        rightmovableObject.transform.position = startPosition2;
    }
}
