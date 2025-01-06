using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SimplePlayUI : MonoBehaviour
{
    [Header("버튼 설정")]
    [SerializeField] private Button btnPause;
    [SerializeField] private Button btnResume;
    [SerializeField] private Slider volumeSlider;

    [Header("체크박스 설정")]
    [SerializeField] private Toggle fireCheckBox;
    [SerializeField] private Toggle ClearPointCheckBox;
    [SerializeField] private GameObject panel;

    [Header("게임 관련")]
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private FireSpawn fireSpawn;

    [SerializeField] private GameObject blinkPanel;
    [Header("컷씬 이미지 리스트와 컷별 등장시간 설정")]
    [SerializeField] private List<Image> cutSceneImages;
    [SerializeField] private float deactivateTime = 3;
    [SerializeField] private GameObject ClearPoint;
    [SerializeField] private GameObject OptionPanel;
    [SerializeField] private GameObject display;
    [SerializeField] private GameObject GameOverImage;
    [SerializeField] private GameObject clearImage;

    private int currentCutSceneIndex = 0;
    private bool isPaused = false;

    private void Start()
    {
        InitializeUI();

        btnPause.onClick.AddListener(PauseGame);
        btnResume.onClick.AddListener(ResumeGame);
        volumeSlider.onValueChanged.AddListener(SetVolume);

        if (cutSceneImages == null || cutSceneImages.Count == 0)
        {
            Debug.LogError("컷씬 이미지 리스트가 비어있습니다!");
            return;
        }

        foreach (var image in cutSceneImages)
        {
            image.gameObject.SetActive(false);
        }

        // GameManager의 startT와 repeatT를 활용
        float startTime = GameManager.Instance.startT;
        float repeatInterval = GameManager.Instance.repeatT;
        StartCoroutine(ActivateCutScenes(startTime, repeatInterval));
    }

    private void Update()
    {
        CheckFireCount();
    }

    private void InitializeUI()
    {
        GameOverImage?.SetActive(false);
        clearImage?.SetActive(false);

        if (btnResume != null) btnResume.gameObject.SetActive(false);
        if (volumeSlider != null)
        {
            volumeSlider.gameObject.SetActive(false);
            volumeSlider.value = AudioListener.volume;
        }

        if (fireCheckBox != null) fireCheckBox.isOn = false;
        if (ClearPointCheckBox != null) ClearPointCheckBox.isOn = false;

        if (blinkPanel != null) blinkPanel.gameObject.SetActive(false);
        if (ClearPoint != null) ClearPoint.gameObject.SetActive(false);
        if (OptionPanel != null) OptionPanel.gameObject.SetActive(false);
        if (display != null) display.gameObject.SetActive(false);
    }

    private void CheckFireCount()
    {
        if (fireSpawn != null && fireSpawn.fireCount == 0 && !fireCheckBox.isOn)
        {
            fireCheckBox.isOn = true;
            ClearPoint?.SetActive(true);
            Debug.Log("모든 불이 꺼졌습니다.");
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        gameTimer?.PauseTimer();
        ToggleUIForPause(true);
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        gameTimer?.StartTimer();
        ToggleUIForPause(false);
    }

    private void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    private void ToggleUIForPause(bool isPaused)
    {
        OptionPanel?.SetActive(isPaused);
        btnPause?.gameObject.SetActive(!isPaused);
        btnResume?.gameObject.SetActive(isPaused);
        volumeSlider?.gameObject.SetActive(isPaused);
    }

    private IEnumerator ActivateCutScenes(float startDelay, float interval)
    {
        yield return new WaitForSeconds(startDelay);

        while (currentCutSceneIndex < cutSceneImages.Count)
        {
            ActivateCutScene(currentCutSceneIndex);
            currentCutSceneIndex++;
            yield return new WaitForSeconds(interval);
        }
    }

    private void ActivateCutScene(int index)
    {
        if (index >= 0 && index < cutSceneImages.Count)
        {
            cutSceneImages[index].gameObject.SetActive(true);
            StartCoroutine(DeactivateCutScene(index, deactivateTime));
        }
    }

    private IEnumerator DeactivateCutScene(int index, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (index >= 0 && index < cutSceneImages.Count)
        {
            cutSceneImages[index].gameObject.SetActive(false);
        }
    }
}
