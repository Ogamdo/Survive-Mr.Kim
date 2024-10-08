using UnityEngine;
using System.Collections;

public class ScreenBlink : MonoBehaviour
{
    public GameObject blackPanel;
    
    void Start()
    {
        if (blackPanel != null)
        {
            StartCoroutine(DelayedBlink(6f, 7f)); // 6�� ��� �� 7�� ���� ������
        }
    }

    IEnumerator DelayedBlink(float delay, float duration)
    {
        // �ʱ� 6�� ���
        yield return new WaitForSeconds(delay);

        // 7�� ���� ������
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
