using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (!child.GetComponent<Collider>())
            {
                // Collider�� �߰��մϴ�.
                CapsuleCollider capsuleCollider = child.gameObject.AddComponent<CapsuleCollider>();

                capsuleCollider.center = new Vector3(0f, 1f, 0f); // ���� ������ �����Ͻʽÿ�.
                capsuleCollider.height = 2f; // ���� ������ �����Ͻʽÿ�.
            }

            if (!child.GetComponent<Rigidbody>())
            {
                // Rigidbody�� �߰��մϴ�.
                Rigidbody rigidbody = child.gameObject.AddComponent<Rigidbody>();

                // Rigidbody ������ ���ϴ� ��� �����մϴ�.
                rigidbody.mass = 1f; // ���� ������ �����Ͻʽÿ�.
                rigidbody.drag = 0.5f; // ���� ������ �����Ͻʽÿ�.
                rigidbody.angularDrag = 0.5f; // ���� ������ �����Ͻʽÿ�.

                rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            }

        }
    }
}
