using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float playTimeLimit = 120.0f;  // ���� ���� �ð� (�� ����)
    private bool isGameActive = false;    // ���� ���¸� Ȯ���ϱ� ���� ����
    private GameManager gameManager;
    
    // **SimplePlayUI���� ����� UI �ؽ�Ʈ ����**
    public Text timerText;                // ���� �ð��� ǥ���� Text UI

    void Start()
    {
        // GameManager �ν��Ͻ��� �����ɴϴ�.
        gameManager = GameManager.Instance;
        UpdateTimerUI();                  // �ʱ�ȭ �� ���� �ð��� UI�� ǥ���մϴ�.
    }

    void Update()
    {
        // ������ Ȱ�� ������ ���� Ÿ�̸Ӹ� ���ҽ�ŵ�ϴ�.
        if (isGameActive)
        {
            if (playTimeLimit > 0)
            {
                playTimeLimit -= Time.deltaTime;
                UpdateTimerUI();          // �ð��� ������ ������ UI�� ������Ʈ�մϴ�.
            }
            else
            {
                EndGame();                // �ð��� 0 ���ϰ� �Ǹ� ������ �����մϴ�.
            }
        }
    }

    public void UpdateTimerUI()
    {
        // **���� �ð��� UI�� ǥ���ϴ� �Լ�**
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(playTimeLimit).ToString();
        }
    }

    void EndGame()
    {
        isGameActive = false;
        Debug.Log("���� ����! �ð��� ��� �����Ǿ����ϴ�.");

        // GameManager�� EndGame �޼��带 ȣ���Ͽ� ���� ���Ḧ ó���մϴ�.
        if (gameManager != null)
        {
            gameManager.EndGame();
        }
    }

    // **���� �Ͻ����� ���**
    public void PauseTimer()
    {
        isGameActive = false; // Ÿ�̸Ӹ� ����ϴ�.
    }

    // **���� �簳 ���**
    public void StartTimer()
    {
        isGameActive = true; // Ÿ�̸Ӹ� �����Ͽ� ������ Ȱ�� ���·� ����ϴ�.
    }

    public void RestartGame()
    {
        playTimeLimit = 120.0f; // ���� �ð��� �ٽ� �ʱ�ȭ
        isGameActive = true;
        UpdateTimerUI();        // UI�� ������Ʈ�Ͽ� �ʱ� ���¸� ǥ���մϴ�.
    }

    public void SetPlayTimeLimit(float newLimit)
    {
        playTimeLimit = newLimit; // ���ο� ���� �ð��� ����
        UpdateTimerUI();          // UI�� ������Ʈ�Ͽ� ����� �ð��� ǥ���մϴ�.
    }

    public bool IsGameActive()
    {
        return isGameActive;      // ���� ������ Ȱ�� �������� ��ȯ�մϴ�.
    }
}
