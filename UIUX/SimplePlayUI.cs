using UnityEngine;
using UnityEngine.UI;

public class SimplePlayUI : MonoBehaviour
{
    // UI 요소: 일시정지, 재개, 볼륨 슬라이더, 체크박스
    [Header("버튼")]
    public Button btnPause;             // 일시정지 버튼
    public Button btnResume;            // 재개 버튼
    public Slider volumeSlider;         // 볼륨 설정 슬라이더
    public GameTimer gameTimer;         // GameTimer 객체 참조

    [Header("체크박스")]
    public Toggle fireCheckBox;         // 불을 끄는 체크박스
    public Toggle doorCheckBox;         // 문 열림 체크박스

    public GameObject panel;            // 체크박스를 포함할 패널
    private FireCount fireSpawn;        // FireCount 스크립트 참조
    private Door door;                  // Door 스크립트 참조
    private GameManager gameManager;    // GameManager 스크립트 참조
    private bool isPaused = false;      // 일시정지 상태 확인용
    private bool doorOpen;              // 문 열림 상태
    private bool gameClear = false;     // 게임 클리어 상태

    void Start()
    {
        // FireCount와 Door 상태 초기화
        if (fireSpawn != null)
            firenum = fireSpawn.fireCount;

        if (door != null)
            doorOpen = door.open;

        // UI 초기화
        btnResume.gameObject.SetActive(false);    // 초기에는 재개 버튼 비활성화
        volumeSlider.gameObject.SetActive(false); // 초기에는 볼륨 슬라이더 비활성화
        volumeSlider.value = AudioListener.volume; // 현재 볼륨을 슬라이더와 동기화

        // 버튼 이벤트 추가
        btnPause.onClick.AddListener(PauseGame);
        btnResume.onClick.AddListener(ResumeGame);
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // 체크박스 초기화 및 부모 패널에 추가
        if (panel != null)
        {
            // Fire 체크박스 설정
            fireCheckBox = new GameObject("FireCheckBox").AddComponent<Toggle>();
            fireCheckBox.transform.SetParent(panel.transform); // Panel의 자식으로 설정
            fireCheckBox.GetComponentInChildren<Text>().text = "불을 모두 꺼주세요"; // 텍스트 설정

            // Door 체크박스 설정
            doorCheckBox = new GameObject("DoorCheckBox").AddComponent<Toggle>();
            doorCheckBox.transform.SetParent(panel.transform); // Panel의 자식으로 설정
            doorCheckBox.GetComponentInChildren<Text>().text = "비상버튼을 눌러 탈출해주세요"; // 텍스트 설정
        }

        // 타이머 UI가 1초마다 갱신되도록 설정
        if (gameTimer != null)
        {
            InvokeRepeating("UpdateTimerUI", 0f, 1f); // 1초 간격으로 UI 갱신
        }
    }

    void Update()
    {
        // 게임 상태 체크
        CheckFireCount();
        CheckDoorOpen();
        CheckGameClear();
    }

    // 불을 모두 끈 상태인지 확인
    void CheckFireCount()
    {
        if (fireSpawn != null && fireSpawn.fireCount == 0)
        {
            fireCheckBox.isOn = true; // 체크박스 활성화
            Debug.Log("fireCount가 0입니다. FireCheckBox가 활성화됩니다.");
        }
    }

    // 문이 열렸는지 확인
    void CheckDoorOpen()
    {
        if (door != null && door.open && !doorCheckBox.isOn)
        {
            doorCheckBox.isOn = true; // 체크박스 활성화
            Debug.Log("문이 열렸습니다. DoorCheckBox가 활성화됩니다.");
        }
    }

    // 게임 클리어 조건 확인
    void CheckGameClear()
    {
        if (fireCheckBox.isOn && doorCheckBox.isOn && !gameClear)
        {
            gameClear = true; // 게임 클리어 상태로 설정
            if (gameManager != null)
                gameManager.EndGame(true); // 게임 종료 처리
            Debug.Log("게임 클리어!");
        }
    }

    // 타이머 UI 갱신
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
        gameTimer?.PauseTimer(); // 타이머 정지
        btnPause.gameObject.SetActive(false); // 일시정지 버튼 숨김
        btnResume.gameObject.SetActive(true); // 재개 버튼 표시
        volumeSlider.gameObject.SetActive(true); // 볼륨 슬라이더 표시
        Debug.Log("게임이 일시정지되었습니다.");
    }

    // 게임 재개
    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; // 게임 시간을 정상 속도로 설정
        gameTimer?.StartTimer(); // 타이머 시작
        btnPause.gameObject.SetActive(true); // 일시정지 버튼 표시
        btnResume.gameObject.SetActive(false); // 재개 버튼 숨김
        volumeSlider.gameObject.SetActive(false); // 볼륨 슬라이더 숨김
        Debug.Log("게임이 재개되었습니다.");
    }

    // 볼륨 설정 변경
    void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        Debug.Log($"볼륨이 {volume}로 설정되었습니다.");
    }

    void OnDestroy()
    {
        // 스크립트가 파괴될 때 반복 호출 중지
        CancelInvoke("UpdateTimerUI");
    }
}
