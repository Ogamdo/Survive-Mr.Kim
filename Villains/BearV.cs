using UnityEngine;
using System.Collections;

public class BearV : MonoBehaviour
{
    public float hugDuration = 2.0f;          // ���ȱ� ���� �ð�
    public int requiredPressCount = 10;       // ���ȱ⿡�� ����� ���� �����̽��� ���� Ƚ��

    private bool isHugging = false;           // ���ȱ� ���� Ȯ��
    private GameObject player;                // �÷��̾� ������Ʈ ����
    private float hugTimer;                   // ���ȱ� Ÿ�̸�
    private int spacePressCount;              // �����̽��� ���� Ƚ��

    private float originalSpeed;              // ���� �÷��̾� �ӵ�
    private CharMove playerMovement;          // �÷��̾� �̵� ��ũ��Ʈ ����

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isHugging)
        {
            player = other.gameObject;
            playerMovement = player.GetComponent<CharMove>();
            if (playerMovement != null)
            {
                originalSpeed = playerMovement.speed; // ���� �ӵ� ����
                StartCoroutine(HugPlayer());          // ���ȱ� ����
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            StopCoroutine(HugPlayer());
            EndHug();
        }
    }

    IEnumerator HugPlayer()
    {
        isHugging = true;
        hugTimer = hugDuration;
        spacePressCount = 0;

        // �÷��̾� �ӵ� 0���� �����Ͽ� ������ ����
        playerMovement.SetSpeed(0);

        while (hugTimer > 0 && spacePressCount < requiredPressCount)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                spacePressCount++;
                // UI ������Ʈ ����� �ִٸ� ���⿡ �߰�
            }

            hugTimer -= Time.deltaTime;
            yield return null;
        }

        EndHug();
    }

    void EndHug()
    {
        if (playerMovement != null)
        {
            playerMovement.SetSpeed(originalSpeed); // ���� �ӵ��� ����
        }
        isHugging = false;
    }
}
