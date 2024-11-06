using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireEx : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject targetObject; // 내려놓을 위치로 사용할 오브젝트
    private bool shouldFollowGun = false;
    private bool isInTrigger = false;
    public FireEx fireExScript;
    public GameObject Deleteobj; // 철자 오류 수정

    void Update()
    {
        // 트리거 범위 내에서 F 키를 누르면 따라가기/내려놓기 전환
        if (isInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            shouldFollowGun = !shouldFollowGun; // 상태 전환

            if (shouldFollowGun)
            {
                PlaceObjectAtGunPosition(); // 오브젝트를 총 위치에 배치

                // Deleteobj가 존재할 경우 삭제
                if (Deleteobj != null)
                {
                    Destroy(Deleteobj); // 지정된 오브젝트 삭제
                }

                // FireEx 스크립트 활성화
                if (fireExScript != null)
                {
                    fireExScript.Activate();
                }
            }
            else
            {
                ReleaseObject(); // 오브젝트 내려놓기
            }
        }
    }
    // 특정 트리거 범위에 들어갔을 때 isInTrigger를 true로 설정
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EX")
        {
            isInTrigger = true;
        }
    }
    // 특정 트리거 범위를 벗어났을 때 isInTrigger를 false로 설정
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "EX")
        {
            isInTrigger = false;
        }
    }

    void PlaceObjectAtGunPosition()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Transform gunTransform = player.transform.Find("Gun");// 총 위치 찾기
            if (gunTransform != null)
            {
                // 오브젝트의 X 축 회전값을 유지
                float originalXRotation = objectToPlace.transform.eulerAngles.x;
                // 총 위치에 오브젝트 배치
                objectToPlace.transform.SetParent(gunTransform);
                objectToPlace.transform.localPosition = Vector3.zero;
                // X 축 회전값을 유지하면서 총 위치의 회전값을 적용
                Vector3 newRotation = gunTransform.eulerAngles;
                newRotation.x = originalXRotation;
                objectToPlace.transform.eulerAngles = newRotation;

                // CharMove 컴포넌트를 통해 속도 설정 (오브젝트 들었을 때 속도 조정)
                CharMove charMove = player.GetComponent<CharMove>();
                if (charMove != null)
                {
                    charMove.SetSpeed(2f);
                }
            }
            else
            {
                Debug.LogError("Gun 오브젝트를 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogError("Player 태그를 가진 오브젝트를 찾을 수 없습니다.");
        }
    }

    // 오브젝트를 내려놓는 메서드
    void ReleaseObject()
    {
        objectToPlace.transform.SetParent(null); // 부모 해제하여 독립된 오브젝트로 만듦

        // targetObject 위치로 오브젝트 배치
        if (targetObject != null)
        {
            objectToPlace.transform.position = targetObject.transform.position;
        }
        else
        {
            Debug.LogWarning("targetObject가 할당되지 않았습니다.");
            objectToPlace.transform.position = new Vector3(0, 1, 0); // 기본 위치로 설정
        }

        // FireEx 스크립트 비활성화
        if (fireExScript != null)
        {
            fireExScript.Deativate();
        }

        // CharMove 속도를 원래 값으로 복구
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            CharMove charMove = player.GetComponent<CharMove>();
            if (charMove != null)
            {
                charMove.SetSpeed(4f); // 원래 속도로 복구
            }
        }
    }
}
