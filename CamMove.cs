using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public Transform target;
    float dist = 4.0f;
    float height = 10.0f;
    Transform tr;
    void Start()
    {
        tr = GetComponent<Transform>();
    }
    void LateUpdate()
    {
        float yAngle = Mathf.LerpAngle(tr.eulerAngles.y, target.eulerAngles.y, Time.deltaTime);
        Quaternion rot = Quaternion.Euler(0, yAngle, 0);
        tr.position = target.position - (rot * Vector3.forward * dist) + (Vector3.up * height);
        tr.LookAt(target);
    }
}
