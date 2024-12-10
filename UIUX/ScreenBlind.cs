using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBlind : MonoBehaviour
{
    [SerializeField] public GameObject blackPanel;

    void Start()
    {
        if (blackPanel != null)
        {
            StartCoroutine(DelayedBlink(4f, 1f)); // 6초 대기 후 7초 동안 깜빡임
        }
    }

    IEnumerator DelayedBlink(float delay, float duration)
    {
        // 초기 6초 대기
        yield return new WaitForSeconds(delay);

        // 1초 동안 깜빡임
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            blackPanel.SetActive(!blackPanel.activeSelf); // 현재 상태의 반대로 설정
            yield return new WaitForSeconds(1f); // 1초 대기
        }

        // 깜빡임이 끝난 후에는 패널을 비활성화 상태로 설정
        blackPanel.SetActive(false);
    }
}
