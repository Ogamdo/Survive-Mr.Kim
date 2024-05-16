using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float playTimeLimit = 60.0f;  // 설정된 시간 (초 단위)
    private bool isGameActive = true;
    private GameManager gameManager;
    public Text timerText;  // UI Text 요소를 참조

    void Start()
    {
        // GameManager 오브젝트를 찾습니다.
        gameManager = FindObjectOfType<GameManager>();
        if (timerText == null)
        {
            Debug.LogError("TimerText가 설정되지 않았습니다.");
        }

        UpdateTimerUI(); // 초기화 시 설정된 시간을 표시
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
        Debug.Log("게임 종료! 제한 시간이 초과되었습니다.");

        // GameManager의 EndGame 메서드를 호출하여 UI를 활성화합니다.
        if (gameManager != null)
        {
            gameManager.EndGame();
        }
    }

    public void RestartGame()
    {
        playTimeLimit = 60.0f; // 설정된 시간을 다시 초기화
        isGameActive = true;
        UpdateTimerUI();  // UI 업데이트
    }

    public void SetPlayTimeLimit(float newLimit)
    {
        playTimeLimit = newLimit;
        UpdateTimerUI();  // 새 제한 시간에 따라 UI 업데이트
    }

    public bool IsGameActive()
    {
        return isGameActive;
    }
}
