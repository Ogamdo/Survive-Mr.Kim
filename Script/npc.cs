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
                // Collider를 추가합니다.
                CapsuleCollider capsuleCollider = child.gameObject.AddComponent<CapsuleCollider>();

                capsuleCollider.center = new Vector3(0f, 1f, 0f); // 예시 값으로 설정하십시오.
                capsuleCollider.height = 2f; // 예시 값으로 설정하십시오.
            }

            if (!child.GetComponent<Rigidbody>())
            {
                // Rigidbody를 추가합니다.
                Rigidbody rigidbody = child.gameObject.AddComponent<Rigidbody>();

                // Rigidbody 설정을 원하는 대로 조정합니다.
                rigidbody.mass = 1f; // 예시 값으로 설정하십시오.
                rigidbody.drag = 0.5f; // 예시 값으로 설정하십시오.
                rigidbody.angularDrag = 0.5f; // 예시 값으로 설정하십시오.

                rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            }

        }
    }
}
