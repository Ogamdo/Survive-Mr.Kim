using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float playTimeLimit = 60.0f;  // 게임 시간 (초 단위)
    private bool isGameActive = false;  // 초기 상태를 false로 변경
    private GameManager gameManager;
    public Text timerText;  // UI Text 객체를 참조

    void Start()
    {
        // GameManager 객체를 찾습니다.
        gameManager = FindObjectOfType<GameManager>();
        UpdateTimerUI(); // 초기화 시 시간을 표시합니다.
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
        Debug.Log("게임 종료! 시간이 초과되었습니다.");

        // GameManager의 EndGame 메서드를 호출하여 UI를 비활성화합니다.
        if (gameManager != null)
        {
            gameManager.EndGame();
        }
    }

    public void RestartGame()
    {
        playTimeLimit = 60.0f; // 게임 시간을 다시 초기화
        isGameActive = true;
        UpdateTimerUI();  // UI 업데이트
    }

    public void SetPlayTimeLimit(float newLimit)
    {
        playTimeLimit = newLimit;
        UpdateTimerUI();  // 새 시간 제한을 설정하고 UI 업데이트
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
