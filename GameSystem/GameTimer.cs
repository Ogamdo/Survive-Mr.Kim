using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float playTimeLimit = 120.0f;  // 게임 제한 시간 (초 단위)
    private bool isGameActive = false;    // 게임 상태를 확인하기 위한 변수
    private GameManager gameManager;
    
    // **SimplePlayUI에서 사용할 UI 텍스트 변수**
    public Text timerText;                // 남은 시간을 표시할 Text UI

    void Start()
    {
        // GameManager 인스턴스를 가져옵니다.
        gameManager = GameManager.Instance;
        UpdateTimerUI();                  // 초기화 시 남은 시간을 UI에 표시합니다.
    }

    void Update()
    {
        // 게임이 활성 상태일 때만 타이머를 감소시킵니다.
        if (isGameActive)
        {
            if (playTimeLimit > 0)
            {
                playTimeLimit -= Time.deltaTime;
                UpdateTimerUI();          // 시간이 감소할 때마다 UI를 업데이트합니다.
            }
            else
            {
                EndGame();                // 시간이 0 이하가 되면 게임을 종료합니다.
            }
        }
    }

    public void UpdateTimerUI()
    {
        // **남은 시간을 UI에 표시하는 함수**
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(playTimeLimit).ToString();
        }
    }

    void EndGame()
    {
        isGameActive = false;
        Debug.Log("게임 종료! 시간이 모두 소진되었습니다.");

        // GameManager의 EndGame 메서드를 호출하여 게임 종료를 처리합니다.
        if (gameManager != null)
        {
            gameManager.EndGame();
        }
    }

    // **게임 일시정지 기능**
    public void PauseTimer()
    {
        isGameActive = false; // 타이머를 멈춥니다.
    }

    // **게임 재개 기능**
    public void StartTimer()
    {
        isGameActive = true; // 타이머를 시작하여 게임을 활성 상태로 만듭니다.
    }

    public void RestartGame()
    {
        playTimeLimit = 120.0f; // 게임 시간을 다시 초기화
        isGameActive = true;
        UpdateTimerUI();        // UI를 업데이트하여 초기 상태를 표시합니다.
    }

    public void SetPlayTimeLimit(float newLimit)
    {
        playTimeLimit = newLimit; // 새로운 제한 시간을 설정
        UpdateTimerUI();          // UI를 업데이트하여 변경된 시간을 표시합니다.
    }

    public bool IsGameActive()
    {
        return isGameActive;      // 현재 게임이 활성 상태인지 반환합니다.
    }
}
