using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float playTimeLimit = 60.0f;  // ������ �ð� (�� ����)
    private bool isGameActive = true;
    private GameManager gameManager;
    public Text timerText;  // UI Text ��Ҹ� ����

    void Start()
    {
        // GameManager ������Ʈ�� ã���ϴ�.
        gameManager = FindObjectOfType<GameManager>();
        if (timerText == null)
        {
            Debug.LogError("TimerText�� �������� �ʾҽ��ϴ�.");
        }

        UpdateTimerUI(); // �ʱ�ȭ �� ������ �ð��� ǥ��
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time Limit : " + Mathf.CeilToInt(playTimeLimit).ToString() + "s";
        }
    }

    void Update()
    {
        if (isGameActive)
        {
            if (playTimeLimit > 0)
            {
                playTimeLimit -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                EndGame();
            }
        }
    }

    void EndGame()
    {
        isGameActive = false;
        Debug.Log("���� ����! ���� �ð��� �ʰ��Ǿ����ϴ�.");

        // GameManager�� EndGame �޼��带 ȣ���Ͽ� UI�� Ȱ��ȭ�մϴ�.
        if (gameManager != null)
        {
            gameManager.EndGame();
        }
    }

    public void RestartGame()
    {
        playTimeLimit = 60.0f; // ������ �ð��� �ٽ� �ʱ�ȭ
        isGameActive = true;
        UpdateTimerUI();  // UI ������Ʈ
    }

    public void SetPlayTimeLimit(float newLimit)
    {
        playTimeLimit = newLimit;
        UpdateTimerUI();  // �� ���� �ð��� ���� UI ������Ʈ
    }

    public bool IsGameActive()
    {
        return isGameActive;
    }
}
