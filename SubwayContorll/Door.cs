using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // 움직일 수 있는 왼쪽 오브젝트 배열
    public GameObject[] leftmovableObjects;
    // 움직일 수 있는 오른쪽 오브젝트 배열
    public GameObject[] rightmovableObjects;

    // 오른쪽으로 이동할 방향 벡터 (기본값: z축으로 -2만큼 이동)
    public Vector3 rightmoveDirection = new Vector3(0, 0, -2);
    // 왼쪽으로 이동할 방향 벡터 (기본값: z축으로 2만큼 이동)
    public Vector3 leftmoveDirection = new Vector3(0, 0, 2);
    // 이동 거리
    public float moveDistance = 1.0f;
    // 이동에 걸리는 시간
    public float moveDuration = 1.0f;
    private bool open = false;

    // Start는 스크립트가 활성화될 때 한 번 호출됨
    void Start()
    {
        // 문을 이동시키는 코루틴을 시작
        StartCoroutine(MoveObject());
    }

    // Update는 매 프레임마다 호출됨
    void Update()
    {
        // 현재 씬의 모든 게임 오브젝트를 검색하여 Fire 오브젝트가 존재하는지 확인
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        bool fireObjectExists = false;

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "LargeFlames(Clone)") // 이름이 LargeFlames(Clone)인 오브젝트 확인
            {
                fireObjectExists = true; // Fire 오브젝트가 존재함을 표시
                break;
            }
        }

        // Fire 오브젝트가 없을 경우에만 문 동작 수행 가능
        if (!fireObjectExists)
        {
            // 마우스 왼쪽 버튼 클릭 이벤트 감지
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse button clicked");

                // 마우스 클릭 위치에서 광선(Ray)을 발사
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Ray가 오브젝트와 충돌했는지 확인
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Raycast hit something");

                    // Ray가 이 스크립트가 붙어 있는 오브젝트를 맞췄는지 확인
                    if (hit.transform == transform)
                    {
                        Debug.Log("Raycast hit the target transform");
                        // 문을 이동시키는 코루틴 실행
                        StartCoroutine(MoveObject());
                    }
                }
                else
                {
                    Debug.Log("Raycast did not hit anything");
                }
            }
        }
        else
        {
            Debug.Log("Fire objects found, action blocked");
        }
    }

    // 문을 이동시키는 코루틴
    IEnumerator MoveObject()
    {
        // 왼쪽 오브젝트와 오른쪽 오브젝트의 시작 및 끝 위치를 저장할 배열
        Vector3[] leftStartPositions = new Vector3[leftmovableObjects.Length];
        Vector3[] rightStartPositions = new Vector3[rightmovableObjects.Length];
        Vector3[] leftEndPositions = new Vector3[leftmovableObjects.Length];
        Vector3[] rightEndPositions = new Vector3[rightmovableObjects.Length];
       
        // 왼쪽 오브젝트의 시작 위치와 끝 위치 설정
        for (int i = 0; i < leftmovableObjects.Length; i++)
        {
            leftStartPositions[i] = leftmovableObjects[i].transform.position; // 현재 위치 저장
            leftEndPositions[i] = leftStartPositions[i] + (leftmoveDirection * moveDistance); // 이동 후 위치 계산
        }

        // 오른쪽 오브젝트의 시작 위치와 끝 위치 설정
        for (int i = 0; i < rightmovableObjects.Length; i++)
        {
            rightStartPositions[i] = rightmovableObjects[i].transform.position; // 현재 위치 저장
            rightEndPositions[i] = rightStartPositions[i] + (rightmoveDirection * moveDistance); // 이동 후 위치 계산
        }

        float elapsedTime = 0; // 경과 시간
        float timeToReturn = 5f; // 이동 후 대기 시간

        // 문이 열리는 애니메이션 수행
        while (elapsedTime < moveDuration)
        {
            for (int i = 0; i < leftmovableObjects.Length; i++)
            {
                // 왼쪽 오브젝트를 시작 위치에서 끝 위치로 선형 보간하며 이동
                leftmovableObjects[i].transform.position = Vector3.Lerp(leftStartPositions[i], leftEndPositions[i], elapsedTime / moveDuration);
            }

            for (int i = 0; i < rightmovableObjects.Length; i++)
            {
                // 오른쪽 오브젝트를 시작 위치에서 끝 위치로 선형 보간하며 이동
                rightmovableObjects[i].transform.position = Vector3.Lerp(rightStartPositions[i], rightEndPositions[i], elapsedTime / moveDuration);
            }

            elapsedTime += Time.deltaTime; // 경과 시간 업데이트
            yield return null; // 다음 프레임까지 대기
        }

        // 이동 완료 후 대기
        yield return new WaitForSeconds(timeToReturn);
         open = true;
        // 문이 닫히는 애니메이션 수행
        float returnDuration = 1.0f; // 닫히는 데 걸리는 시간
        float returnElapsedTime = 0;

        while (returnElapsedTime < returnDuration)
        {
            for (int i = 0; i < leftmovableObjects.Length; i++)
            {
                // 왼쪽 오브젝트를 끝 위치에서 시작 위치로 선형 보간하며 이동
                leftmovableObjects[i].transform.position = Vector3.Lerp(leftEndPositions[i], leftStartPositions[i], returnElapsedTime / returnDuration);
            }

            for (int i = 0; i < rightmovableObjects.Length; i++)
            {
                // 오른쪽 오브젝트를 끝 위치에서 시작 위치로 선형 보간하며 이동
                rightmovableObjects[i].transform.position = Vector3.Lerp(rightEndPositions[i], rightStartPositions[i], returnElapsedTime / returnDuration);
            }
            
            returnElapsedTime += Time.deltaTime; // 경과 시간 업데이트
            yield return null; // 다음 프레임까지 대기
        }

        // 닫기 완료 후 위치를 정확히 초기 위치로 설정
        for (int i = 0; i < leftmovableObjects.Length; i++)
        {
            leftmovableObjects[i].transform.position = leftStartPositions[i];
        }

        for (int i = 0; i < rightmovableObjects.Length; i++)
        {
            rightmovableObjects[i].transform.position = rightStartPositions[i];
        }
    }
}
