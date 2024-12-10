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
            StartCoroutine(DelayedBlink(4f, 1f)); // 6�� ��� �� 7�� ���� ������
        }
    }

    IEnumerator DelayedBlink(float delay, float duration)
    {
        // �ʱ� 6�� ���
        yield return new WaitForSeconds(delay);

        // 1�� ���� ������
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            blackPanel.SetActive(!blackPanel.activeSelf); // ���� ������ �ݴ�� ����
            yield return new WaitForSeconds(1f); // 1�� ���
        }

        // �������� ���� �Ŀ��� �г��� ��Ȱ��ȭ ���·� ����
        blackPanel.SetActive(false);
    }
}
