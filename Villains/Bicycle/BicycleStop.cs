using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BicycleStop : MonoBehaviour
{

    private float StopTimeLimit = 20f;
    public GameTimer gameTimer;
    private bool isStopped = false; 

    private Vector3 lastPosition;
    private Quaternion lastRotate;

    // Start is called before the first frame update
    void Start()
    {
        gameTimer = FindObjectOfType<GameTimer>();
        lastPosition = transform.position;
        lastRotate=transform.rotation;
    }


    // Update is called once per frame
    void Update()
    {
        // ���� �ð��� 20�� ������ �� ���߱�
        if (gameTimer != null && gameTimer.playTimeLimit <= StopTimeLimit && !isStopped)
        {
            StopObject();
        }

        // ���� ���¿����� ��ġ ����
        if (isStopped)
        {
            transform.position = lastPosition;
            transform.rotation = lastRotate;
        }
    }

    void StopObject()
    {
        isStopped = true;
        lastRotate = transform.rotation;
        lastPosition = transform.position; // ���� ��ġ ����
        Debug.Log("������ġ: " + lastPosition);
    }
}
