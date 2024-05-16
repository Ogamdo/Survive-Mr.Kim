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
        float timeToReturn = 5f; // �̵� �� �ǵ��ư� �ð�

        while (elapsedTime < moveDuration)
        {
            // �̵�
            leftmovableObject.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
            rightmovableObject.transform.position = Vector3.Lerp(startPosition2, endPosition2, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �̵� �Ϸ� �� ���
        yield return new WaitForSeconds(timeToReturn);

        // �ǵ��ư���
        float returnDuration = 1.0f; // �ǵ��ư��� �ð�
        float returnElapsedTime = 0;

        while (returnElapsedTime < returnDuration)
        {
            leftmovableObject.transform.position = Vector3.Lerp(endPosition, startPosition, returnElapsedTime / returnDuration);
            rightmovableObject.transform.position = Vector3.Lerp(endPosition2, startPosition2, returnElapsedTime / returnDuration);
            returnElapsedTime += Time.deltaTime;
            yield return null;
        }

        // �ǵ��ư��� �Ϸ� �� ���� ��ġ ����
        leftmovableObject.transform.position = startPosition;
        rightmovableObject.transform.position = startPosition2;
    }
}
