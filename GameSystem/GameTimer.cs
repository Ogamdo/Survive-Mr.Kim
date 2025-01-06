using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Header("타이머 설정")]
    [Tooltip("게임 전체 시간 제한 (초 단위)")]
    public float playTimeLimit = 120.0f; // 게임 시간 제한 (초)

    [Tooltip("남은 시간을 표시할 텍스트 컴포넌트")]
    public Text timerText; // 타이머 UI 텍스트

    private bool isGameActive = false; // 타이머 활성화 여부
    private float currentPlayTime; // 현재 남은 시간

    void Start()
    {
        // 초기화: 설정된 제한 시간으로 타이머 시작
        currentPlayTime = playTimeLimit;
        UpdateTimerUI(); // 초기 UI 업데이트
        StartTimer(); // 타이머 시작
    }

    void FixedUpdate()
    {
        if (isGameActive)
        {
            // 타이머 감소 처리
            if (currentPlayTime > 0)
            {
                currentPlayTime -= Time.fixedDeltaTime;

                // 초 단위로 UI 업데이트
                UpdateTimerUI();

                // 남은 시간이 음수로 내려가지 않도록 보정
                if (currentPlayTime < 0)
                {
                    currentPlayTime = 0;
                }
            }
        }
    }

    /// <summary>
    /// 남은 시간을 UI에 업데이트 (초 단위).
    /// </summary>
    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            // 남은 시간을 반올림하여 초 단위로 텍스트 표시
            timerText.text = Mathf.CeilToInt(currentPlayTime).ToString();
        }
    }

    /// <summary>
    /// 타이머를 시작합니다.
    /// </summary>
    public void StartTimer()
    {
        isGameActive = true;
    }

    /// <summary>
    /// 타이머를 일시 정지합니다.
    /// </summary>
    public void PauseTimer()
    {
        isGameActive = false;
    }

    /// <summary>
    /// 현재 타이머가 활성 상태인지 확인합니다.
    /// </summary>
    /// <returns>타이머 활성 상태 (true/false)</returns>
    public bool IsGameActive() => isGameActive;
}
