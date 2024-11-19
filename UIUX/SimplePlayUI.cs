using UnityEngine;
using UnityEngine.UI;

public class SimplePlayUI : MonoBehaviour
{
    // UI 요소: 일시정지, 재개, 사운드 슬라이더
    public Button btnPause; // 일시정지 버튼
    public Button btnResume; // 재개 버튼
    public Slider volumeSlider; // 사운드 조절 슬라이더
 public GameTimer gameTimer; // GameTimer 객체를 에디터에서 할당

    private bool isPaused = false; // 일시정지 상태 확인용

    void Start()
    {   
        // UI 초기 설정
        btnResume.gameObject.SetActive(false); // 처음에는 재개 버튼 비활성화
        volumeSlider.gameObject.SetActive(false); // 처음에는 볼륨 슬라이더 비활성화
        volumeSlider.value = AudioListener.volume; // 현재 볼륨에 맞춰 슬라이더 설정

        // 버튼 및 슬라이더에 이벤트 리스너 추가
        btnPause.onClick.AddListener(PauseGame);
        btnResume.onClick.AddListener(ResumeGame);
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // 타이머 UI를 1초마다 업데이트하도록 설정
        if (gameTimer != null)
        {
            InvokeRepeating("UpdateTimerUI", 0f, 1f); // 1초 간격으로 UI 업데이트
        }
    }

    void UpdateTimerUI()
    {
        if (!isPaused && gameTimer != null && gameTimer.IsGameActive())
        {
            gameTimer.UpdateTimerUI(); // 타이머 UI를 1초마다 업데이트
        }
    }

    // 게임 일시정지
    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; // 게임 시간을 멈춤
        gameTimer.PauseTimer(); // 타이머 멈춤
        btnPause.gameObject.SetActive(false); // 일시정지 버튼 숨김
        btnResume.gameObject.SetActive(true); // 재개 버튼 표시
        volumeSlider.gameObject.SetActive(true); // 볼륨 슬라이더 표시
        Debug.Log("게임 일시정지");
    }

    // 게임 재개
    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; // 게임 시간을 정상 속도로 재개
        gameTimer.StartTimer(); // 타이머 재개
        btnPause.gameObject.SetActive(true); // 일시정지 버튼 표시
        btnResume.gameObject.SetActive(false); // 재개 버튼 숨김
        volumeSlider.gameObject.SetActive(false); // 볼륨 슬라이더 숨김
        Debug.Log("게임 재개");
    }

    // 사운드 볼륨 조절
    void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        Debug.Log("사운드 볼륨 설정됨: " + volume);
    }

    void OnDestroy()
    {
        // 이 스크립트가 파괴될 때 InvokeRepeating 중지
        CancelInvoke("UpdateTimerUI");
    }
}
