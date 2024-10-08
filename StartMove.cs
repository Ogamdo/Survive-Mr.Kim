using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMove : MonoBehaviour
{
    public Transform Point1; // ���� ����
    public Transform Point2; // ���� ����
    public float moveSpeed = 5f; // �̵� �ӵ� (����: ����/��)
    private Animator anim; // Animator ���� �߰�

    void Start()
    {
        // Animator �ʱ�ȭ
        anim = GetComponent<Animator>();

        // ���� ���۰� ���ÿ� �̵� �ڷ�ƾ ����
        StartCoroutine(MoveObject(Point1.position, Point2.position, moveSpeed));
    }

    IEnumerator MoveObject(Vector3 startPoint, Vector3 endPoint, float speed)
    {
        float distance = Vector3.Distance(startPoint, endPoint);
        float duration = distance / speed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // ������Ʈ ��ġ�� �̵�
            transform.position = Vector3.Lerp(startPoint, endPoint, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            anim.SetBool("Walk", true);
            yield return null;
        }

        // ���� ��ġ ����
        transform.position = endPoint;
        
    }
}
