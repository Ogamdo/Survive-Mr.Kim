using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [Tooltip("Total time limit for the game in seconds")]
    public float playTimeLimit = 120.0f;

    [Tooltip("Text component to display the remaining time")]
    public Text timerText;

    private bool isGameActive = false;
    [HideInInspector]public float currentPlayTime; // To prevent modifying the original playTimeLimit

    void Start()
    {
        currentPlayTime = playTimeLimit;
        UpdateTimerUI();
        StartTimer();
    }

    void FixedUpdate() // Use FixedUpdate for consistent time-based operations
    {
        if (isGameActive)
        {
            if (currentPlayTime > 0)
            {
                currentPlayTime -= Time.fixedDeltaTime;
                UpdateTimerUI();
            }
            else
            {
                EndGame(false);
            }
        }
    }

    public void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(currentPlayTime).ToString();
        }
    }

    public void StartTimer()
    {
        isGameActive = true;
    }

    public void PauseTimer()
    {
        isGameActive = false;
    }

    private void EndGame()
    {
        isGameActive = false;
        Debug.Log("Game Over! Time is up.");
    }

    public bool IsGameActive() => isGameActive;
}
