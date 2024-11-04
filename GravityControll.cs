using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour
{
    private Rigidbody rb; // Script�� ����� Object�� Rigidbody
    private Transform tr; // Script�� ����� Object�� Transform
    [SerializeField]  float checkDistance = 2.0f; // Raycast �Ÿ�
    [SerializeField]  LayerMask groundLayer; // �ٴ� ���̾�

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        CheckGround();
    }

 

    void CheckGround()
    {
        // ĳ������ ��ġ���� �Ʒ��� Raycast �߻�
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, checkDistance, groundLayer))
        {
            // Raycast�� �ٴ��� �������� �� ����
            Vector3 groundPosition = hit.point;
            groundPosition.y += rb.transform.localScale.y / 2; // ������Ʈ ���� ����

            // ĳ���Ͱ� �ٴں��� �Ʒ��� ���� �� ��ġ ����
            if (tr.position.y < groundPosition.y)
            {
                tr.position = groundPosition; // ������Ʈ ��ġ ����
            }
        }
    }
}
