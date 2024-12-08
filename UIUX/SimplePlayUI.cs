using UnityEngine;
using UnityEngine.UI;

public class SimplePlayUI : MonoBehaviour
{
    // UI 요소: 일시정지, 재개, 볼륨 슬라이더, 체크박스
    public Button btnPause; // 일시정지 버튼
    public Button btnResume; // 재개 버튼
    public Slider volumeSlider; // 볼륨 설정 슬라이더
    public GameTimer gameTimer; // GameTimer 객체의 참조

    public Toggle checkbox1; // 체크박스 1
    public Toggle checkbox2; // 체크박스 2
    public GameObject panel; // 체크박스를 포함할 패널

    private bool isPaused = false; // 일시정지 상태 확인용

    void Start()
    {
        // UI 초기화
        btnResume.gameObject.SetActive(false); // 초기에는 재개 버튼 비활성화
        volumeSlider.gameObject.SetActive(false); // 초기에는 볼륨 슬라이더 비활성화
        volumeSlider.value = AudioListener.volume; // 현재 볼륨을 슬라이더에 동기화

        // 버튼과 슬라이더 이벤트 추가
        btnPause.onClick.AddListener(PauseGame);
        btnResume.onClick.AddListener(ResumeGame);
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // 체크박스 초기화 및 부모 패널에 추가
        if (panel != null)
        {
            // 체크박스 1 설정
            checkbox1 = new GameObject("Checkbox1").AddComponent<Toggle>();
            checkbox1.transform.SetParent(panel.transform); // Panel의 자식으로 설정
            checkbox1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -30); // 위치 설정
            checkbox1.GetComponentInChildren<Text>().text = "옵션 1"; // 텍스트 설정

            // 체크박스 2 설정
            checkbox2 = new GameObject("Checkbox2").AddComponent<Toggle>();
            checkbox2.transform.SetParent(panel.transform); // Panel의 자식으로 설정
            checkbox2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -60); // 위치 설정
            checkbox2.GetComponentInChildren<Text>().text = "옵션 2"; // 텍스트 설정
        }

        // 타이머 UI가 1초마다 갱신되도록 설정
        if (gameTimer != null)
        {
            InvokeRepeating("UpdateTimerUI", 0f, 1f); // 1초 간격으로 UI 갱신
        }
    }

    void UpdateTimerUI()
    {
        if (!isPaused && gameTimer != null && gameTimer.IsGameActive())
        {
            gameTimer.UpdateTimerUI(); // 타이머 UI를 1초마다 갱신
        }
    }

    // 게임 일시정지
    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; // 게임 시간을 멈춤
        gameTimer.PauseTimer(); // 타이머 정지
        btnPause.gameObject.SetActive(false); // 일시정지 버튼 숨김
        btnResume.gameObject.SetActive(true); // 재개 버튼 표시
        volumeSlider.gameObject.SetActive(true); // 볼륨 슬라이더 표시
        Debug.Log("게임 일시정지");
    }

    // 게임 재개
    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; // 게임 시간을 정상 속도로 설정
        gameTimer.StartTimer(); // 타이머 시작
        btnPause.gameObject.SetActive(true); // 일시정지 버튼 표시
        btnResume.gameObject.SetActive(false); // 재개 버튼 숨김
        volumeSlider.gameObject.SetActive(false); // 볼륨 슬라이더 숨김
        Debug.Log("게임 재개");
    }

    // 볼륨 설정 변경
    void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        Debug.Log("볼륨 설정 변경됨: " + volume);
    }

    void OnDestroy()
    {
        // 이 스크립트가 파괴될 때 InvokeRepeating 중지
        CancelInvoke("UpdateTimerUI");
    }
}
