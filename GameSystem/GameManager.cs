using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("빌런 세팅")]
    private GameTimer gameTimer;
    [SerializeField] public float startT = 15f; // 빌런 스폰 시작 시간
    [SerializeField] public float repeatT = 15f; // 빌런 스폰 간격
    [SerializeField] private List<GameObject> villains = new List<GameObject>(); // 빌런 리스트

    [Header("클리어 문구와 사망 문구 세팅")]
    public GameObject youSurvivedText; // "You Survived" 텍스트 오브젝트
    public GameObject youDiedText; // "You Died" 텍스트 오브젝트
    public GameObject ClearImage; // 클리어 이미지

    public static GameManager Instance { get; private set; } // GameManager의 싱글톤 인스턴스

    private int currentVillainIndex = 0; // 현재 활성화할 빌런 인덱스

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // 싱글톤 인스턴스 설정
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 파괴
        }
    }

    void Start()
    {
        // 초기 상태로 UI 및 빌런 비활성화
        youDiedText.SetActive(false);
        youSurvivedText.SetActive(false);
        ClearImage.SetActive(false);

        foreach (var villain in villains)
        {
            villain.SetActive(false);
        }

        // 빌런 스폰 시작
        InvokeRepeating(nameof(VillainsSpawn), startT, repeatT);
    }

    public void EndGame(bool clear)
    {
        if (clear)
        {
            youSurvivedText.SetActive(true);
            ClearImage.SetActive(true);
        }
        else
        {
            youDiedText.SetActive(true);
        }
    }

    public void ClearGame()
    {
        youSurvivedText.SetActive(true); // "You Survived" 텍스트 활성화
    }

    private void VillainsSpawn()
    {
        if (currentVillainIndex < villains.Count)
        {
            // 현재 인덱스의 빌런 활성화
            villains[currentVillainIndex].SetActive(true);
            Debug.Log($"{currentVillainIndex}번 빌런 등장!");

            currentVillainIndex++; // 다음 빌런으로 이동
        }
        else
        {
            // 모든 빌런이 등장했으면 반복 중지
            CancelInvoke(nameof(VillainsSpawn));
            Debug.Log("모든 빌런이 활성화되었습니다.");
        }
    }
}
