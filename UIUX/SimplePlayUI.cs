using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class SimplePlayUI : MonoBehaviour
{
    [Header("버튼 설정")]
    [SerializeField] private Button btnPause;         // 일시정지 버튼
    [SerializeField] private Button btnResume;        // 재개 버튼
    [SerializeField] private Slider volumeSlider;     // 볼륨 슬라이더

    [Header("체크박스 설정")]
    [SerializeField] private Toggle fireCheckBox;     // 불 끄기 체크박스
    [SerializeField] private Toggle doorCheckBox;     // 문 열림 체크박스
    [SerializeField] private GameObject panel;        // 체크박스가 포함된 패널

    [Header("게임 관련")]
    [SerializeField] private GameTimer gameTimer;     // GameTimer 객체
    [SerializeField] private GameManager gameManager; // GameManager 객체
    [SerializeField] private FireSpawn fireSpawn;     // FireSpawn 객체
    [SerializeField] private Door door;               // Door 객체
     [Header("컷씬 이미지 리스트와 컷별 등장시간 설정")]
    [SerializeField] private List<Image> cutSceneImages; // 컷씬 이미지 리스트 (UI Image)
    [SerializeField] private float deactivateTime=3;
    private int currentCutSceneIndex = 0; // 현재 활성화된 컷씬 인덱스
    private bool isPaused = false; // 일시정지 상태
    private bool gameClear = false; // 게임 클리어 상태

    

    private void Start()
    {
        // UI 초기화
        InitializeUI();

        // 버튼 이벤트 연결
        btnPause.onClick.AddListener(PauseGame);
        btnResume.onClick.AddListener(ResumeGame);
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // 타이머 UI 주기적 갱신
        if (gameTimer != null)
            InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f);
            
        if (cutSceneImages == null || cutSceneImages.Count == 0)
        {
            Debug.LogError("컷씬 이미지 리스트가 비어있습니다!");
            return;
        }

        // 모든 이미지를 비활성화
        foreach (var image in cutSceneImages)
        {
            image.gameObject.SetActive(false);
        }

        // GameManager의 startT와 repeatT를 가져와 Coroutine 시작
        float startTime = GameManager.Instance.startT;
        float repeatInterval = GameManager.Instance.repeatT;

        StartCoroutine(ActivateCutScenes(startTime, repeatInterval));
    }

    private void Update()
    {
        // 게임 상태 체크
        CheckFireCount();
        CheckDoorOpen();
        CheckGameClear();
    }

    private void InitializeUI()
    {
        if (btnResume != null) btnResume.gameObject.SetActive(false);    // 재개 버튼 숨김
        if (volumeSlider != null)
        {
            volumeSlider.gameObject.SetActive(false);                   // 볼륨 슬라이더 숨김
            volumeSlider.value = AudioListener.volume;                  // 현재 볼륨 동기화
        }

        // 체크박스 초기화
        if (fireCheckBox != null) fireCheckBox.isOn = false;             // Fire 체크박스 초기화
        if (doorCheckBox != null) doorCheckBox.isOn = false;  
        
                   // Door 체크박스 초기화
    }

    // 불이 모두 꺼졌는지 확인
    private void CheckFireCount()
    {
        if (fireSpawn != null && fireSpawn.fireCount == 0 && !fireCheckBox.isOn)
        {
            fireCheckBox.isOn = true; // 체크박스 활성화
            Debug.Log("모든 불이 꺼졌습니다.");
        }
    }

    // 문이 열렸는지 확인
    private void CheckDoorOpen()
    {
        if (door != null && door.open && !doorCheckBox.isOn)
        {
            doorCheckBox.isOn = true; // 체크박스 활성화
            Debug.Log("문이 열렸습니다.");
        }
    }

    // 게임 클리어 상태 확인
    private void CheckGameClear()
    {
        if (fireCheckBox.isOn && doorCheckBox.isOn && !gameClear)
        {
            gameClear = true;
            gameManager?.EndGame(true); // 게임 종료
            Debug.Log("게임 클리어!");
        }
    }

    // 타이머 UI 갱신
    private void UpdateTimerUI()
    {
        if (!isPaused && gameTimer != null && gameTimer.IsGameActive())
        {
            gameTimer.UpdateTimerUI();
        }
    }

    // 게임 일시정지
    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // 게임 멈춤
        gameTimer?.PauseTimer(); // 타이머 멈춤
        ToggleUIForPause(true); // UI 변경
        Debug.Log("게임이 일시정지되었습니다.");
    }

    // 게임 재개
    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // 게임 재개
        gameTimer?.StartTimer(); // 타이머 재개
        ToggleUIForPause(false); // UI 변경
        Debug.Log("게임이 재개되었습니다.");
    }

    // 볼륨 설정
    private void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        Debug.Log($"볼륨이 {volume}로 설정되었습니다.");
    }

    // 일시정지 상태에서 UI 토글
    private void ToggleUIForPause(bool isPaused)
    {
        btnPause?.gameObject.SetActive(!isPaused); // 일시정지 버튼
        btnResume?.gameObject.SetActive(isPaused); // 재개 버튼
        volumeSlider?.gameObject.SetActive(isPaused); // 볼륨 슬라이더
    }

    private void OnDestroy()
    {
        CancelInvoke(nameof(UpdateTimerUI)); // 타이머 갱신 중지
    }
     private IEnumerator ActivateCutScenes(float startDelay, float interval)
    {
        // 시작 딜레이
        yield return new WaitForSeconds(startDelay);

        // 람다식으로 컷씬 활성화 처리
        System.Action<int> activateCutScene = (index) =>
        {
            
            if (index >= 0 && index < cutSceneImages.Count)
            {
                cutSceneImages[index].gameObject.SetActive(true);
                Debug.Log($"컷씬 {index} 활성화");
            
            StartCoroutine(DeactivateCutScene(index, deactivateTime));
            }
        };
        

        // 순차적으로 컷씬 활성화
        while (currentCutSceneIndex < cutSceneImages.Count)
        {
            activateCutScene(currentCutSceneIndex);
            currentCutSceneIndex++;

            // 다음 컷씬을 활성화하기 전까지 대기
            yield return new WaitForSeconds(interval);
        }

        Debug.Log("모든 컷씬이 활성화되었습니다.");
    }
    private IEnumerator DeactivateCutScene(int index, float delay)
{
    yield return new WaitForSeconds(delay);

    if (index >= 0 && index < cutSceneImages.Count)
    {
        cutSceneImages[index].gameObject.SetActive(false);
        Debug.Log($"컷씬 {index} 비활성화");
    }
}
}
