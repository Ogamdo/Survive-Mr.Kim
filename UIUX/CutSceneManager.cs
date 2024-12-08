using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// CutSceneManager
/// - currentPlayTime이 0일 때 첫 컷씬 활성화
/// - 15초마다 이전에 등장하지 않은 컷씬을 순차적으로 활성화
/// - 모든 컷씬이 등장하면 자동 중지
/// </summary>
public class CutSceneManager : MonoBehaviour
{
    [Header("컷씬 이미지 리스트")]
    public List<Image> cutSceneImages; // 컷씬 이미지 리스트 (UI Image)

    [Header("컷씬 설정")]
    public float moveDistance = 100f; // 각 이미지의 이동 거리
    public float displayDuration = 5f; // 각 이미지가 활성화되는 지속 시간
    public float timeInterval = 15f; // 컷씬 등장 간격

    [Header("플레이 타임 (외부에서 갱신)")]
    public float currentPlayTime = 0f; // 게임의 현재 플레이 시간 (외부에서 참조)

    private int currentIndex = 0; // 현재 활성화할 컷씬 인덱스
    private HashSet<int> shownScenes = new HashSet<int>(); // 이미 등장한 컷씬 인덱스
    private Vector3 initialPosition; // 컷씬 초기 위치
    private bool isShowing = false; // 현재 컷씬 활성 상태 확인용

    void Start()
    {
        if (cutSceneImages == null || cutSceneImages.Count == 0)
        {
            Debug.LogError("컷씬 이미지 리스트를 설정해주세요.");
            return;
        }

        // 초기 위치 및 상태 설정
        initialPosition = cutSceneImages[0].rectTransform.anchoredPosition;
        foreach (var image in cutSceneImages)
        {
            image.gameObject.SetActive(false);
            image.rectTransform.anchoredPosition = initialPosition;
        }
    }

    void Update()
    {
        // currentPlayTime이 0이고 아직 첫 컷씬이 안 나왔을 때
        if (currentPlayTime == 0 && shownScenes.Count == 0)
        {
            ShowNextCutScene();
        }

        // 15초마다 새로운 컷씬 등장
        if (currentPlayTime > 0 && currentPlayTime % timeInterval < Time.deltaTime && shownScenes.Count < cutSceneImages.Count)
        {
            ShowNextCutScene();
        }
    }

    public void ShowNextCutScene()
    {
        if (isShowing || shownScenes.Count >= cutSceneImages.Count) return; // 이미 활성화 중이거나 모든 컷씬이 등장했을 경우 종료

        // 다음 등장할 컷씬 인덱스 찾기
        while (shownScenes.Contains(currentIndex))
        {
            currentIndex = (currentIndex + 1) % cutSceneImages.Count;
        }

        // 컷씬 활성화
        var image = cutSceneImages[currentIndex];
        image.gameObject.SetActive(true);
        image.rectTransform.anchoredPosition = initialPosition + Vector3.right * moveDistance * currentIndex;

        isShowing = true;
        shownScenes.Add(currentIndex); // 현재 컷씬을 등장한 리스트에 추가

        // 일정 시간 후 비활성화
        Invoke(nameof(EndCurrentCutScene), displayDuration);
    }

    public void EndCurrentCutScene()
    {
        // 현재 컷씬 비활성화
        cutSceneImages[currentIndex].gameObject.SetActive(false);
        isShowing = false;

        // 다음 컷씬 준비
        currentIndex++;
        if (currentIndex >= cutSceneImages.Count) currentIndex = 0;
    }

    /// <summary>
    /// 컷씬 강제 중단
    /// </summary>
    public void StopCutScenes()
    {
        CancelInvoke(nameof(EndCurrentCutScene));
        isShowing = false;

        // 모든 컷씬 비활성화
        foreach (var image in cutSceneImages)
        {
            image.gameObject.SetActive(false);
        }
    }
}
