using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float playTimeLimit = 60.0f;  // 게임 ?���? (�? ?��?��)
    private bool isGameActive = false;  // 초기 ?��?���? false�? �?�?
    private GameManager gameManager;
    public Text timerText;  // UI Text 객체�? 참조

    void Start()
    {
        // GameManager 객체�? 찾습?��?��.
        gameManager = FindObjectOfType<GameManager>();
        UpdateTimerUI(); // 초기?�� ?�� ?��간을 ?��?��?��?��?��.
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(playTimeLimit).ToString();
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
        Debug.Log("게임 종료! ?��간이 초과?��?��?��?��?��.");

        // GameManager?�� EndGame 메서?���? ?��출하?�� UI�? 비활?��?��?��?��?��.
        if (gameManager != null)
        {
            gameManager.EndGame();
        }
    }

    public void RestartGame()
    {
        playTimeLimit = 60.0f; // 게임 ?��간을 ?��?�� 초기?��
        isGameActive = true;
        UpdateTimerUI();  // UI ?��?��?��?��
    }

    public void SetPlayTimeLimit(float newLimit)
    {
        playTimeLimit = newLimit;
        UpdateTimerUI();  // ?�� ?���? ?��?��?�� ?��?��?���? UI ?��?��?��?��
    }

    public bool IsGameActive()
    {
        return isGameActive;
    }

    public void StartTimer()
    {
        isGameActive = true;
    }
}
