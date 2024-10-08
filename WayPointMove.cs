using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMove : MonoBehaviour
{
    [SerializeField] Transform[] Pos;
    [SerializeField] float speed = 5f;
    int PosNum = 0;

    void Start()
    {
        StartCoroutine(StartMovementAfterDelay(6f)); // 6초 대기 후 이동 시작
    }

    IEnumerator StartMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = Pos[PosNum].position;
        StartCoroutine(MovePath());
    }

    IEnumerator MovePath()
    {
        while (true)
        {
            if (Pos.Length == 0)
                yield break;

            transform.position = Vector3.MoveTowards(transform.position, Pos[PosNum].position, speed * Time.deltaTime);
            transform.LookAt(Pos[PosNum].position);

            if (Vector3.Distance(transform.position, Pos[PosNum].position) < 0.1f)
            {
                PosNum++;
                if (PosNum == Pos.Length)
                {
                    PosNum = 0;
                }
            }

            yield return null;
        }
    }

    // Update는 이제 필요하지 않습니다.
    // void Update() { }
}
