using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireEx : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject targetObject; // �������� ��ġ�� ����� ������Ʈ
    private bool shouldFollowGun = false;
    private bool isInTrigger = false;
    public FireEx fireExScript;
    public GameObject Deleteobj; // ö�� ���� ����

    void Update()
    {
        // Ʈ���� ���� ������ F Ű�� ������ ���󰡱�/�������� ��ȯ
        if (isInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            shouldFollowGun = !shouldFollowGun; // ���� ��ȯ

            if (shouldFollowGun)
            {
                PlaceObjectAtGunPosition(); // ������Ʈ�� �� ��ġ�� ��ġ

                // Deleteobj�� ������ ��� ����
                if (Deleteobj != null)
                {
                    Destroy(Deleteobj); // ������ ������Ʈ ����
                }

                // FireEx ��ũ��Ʈ Ȱ��ȭ
                if (fireExScript != null)
                {
                    fireExScript.Activate();
                }
            }
            else
            {
                ReleaseObject(); // ������Ʈ ��������
            }
        }
    }
    // Ư�� Ʈ���� ������ ���� �� isInTrigger�� true�� ����
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EX")
        {
            isInTrigger = true;
        }
    }
    // Ư�� Ʈ���� ������ ����� �� isInTrigger�� false�� ����
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
            Transform gunTransform = player.transform.Find("Gun");// �� ��ġ ã��
            if (gunTransform != null)
            {
                // ������Ʈ�� X �� ȸ������ ����
                float originalXRotation = objectToPlace.transform.eulerAngles.x;
                // �� ��ġ�� ������Ʈ ��ġ
                objectToPlace.transform.SetParent(gunTransform);
                objectToPlace.transform.localPosition = Vector3.zero;
                // X �� ȸ������ �����ϸ鼭 �� ��ġ�� ȸ������ ����
                Vector3 newRotation = gunTransform.eulerAngles;
                newRotation.x = originalXRotation;
                objectToPlace.transform.eulerAngles = newRotation;

                // CharMove ������Ʈ�� ���� �ӵ� ���� (������Ʈ ����� �� �ӵ� ����)
                CharMove charMove = player.GetComponent<CharMove>();
                if (charMove != null)
                {
                    charMove.SetSpeed(2f);
                }
            }
            else
            {
                Debug.LogError("Gun ������Ʈ�� ã�� �� �����ϴ�.");
            }
        }
        else
        {
            Debug.LogError("Player �±׸� ���� ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    // ������Ʈ�� �������� �޼���
    void ReleaseObject()
    {
        objectToPlace.transform.SetParent(null); // �θ� �����Ͽ� ������ ������Ʈ�� ����

        // targetObject ��ġ�� ������Ʈ ��ġ
        if (targetObject != null)
        {
            objectToPlace.transform.position = targetObject.transform.position;
        }
        else
        {
            Debug.LogWarning("targetObject�� �Ҵ���� �ʾҽ��ϴ�.");
            objectToPlace.transform.position = new Vector3(0, 1, 0); // �⺻ ��ġ�� ����
        }

        // FireEx ��ũ��Ʈ ��Ȱ��ȭ
        if (fireExScript != null)
        {
            fireExScript.Deativate();
        }

        // CharMove �ӵ��� ���� ������ ����
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            CharMove charMove = player.GetComponent<CharMove>();
            if (charMove != null)
            {
                charMove.SetSpeed(4f); // ���� �ӵ��� ����
            }
        }
    }
}
