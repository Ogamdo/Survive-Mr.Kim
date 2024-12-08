using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// CutSceneManager
/// - GameManager의 startT와 repeatT를 기반으로 컷씬 활성화
/// - Coroutine과 람다식을 사용하여 구현
/// </summary>
public class CutSceneManager : MonoBehaviour
{
    [Header("컷씬 이미지 리스트")]
    [SerializeField] private List<Image> cutSceneImages; // 컷씬 이미지 리스트 (UI Image)
    private int currentCutSceneIndex = 0; // 현재 활성화된 컷씬 인덱스

    void Start()
    {
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
}
