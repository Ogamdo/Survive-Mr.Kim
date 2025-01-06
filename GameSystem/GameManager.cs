using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("빌런 설정")]
    [SerializeField] public float startT = 15f; // 빌런 스폰 시작 시간
    [SerializeField] public float repeatT = 15f; // 빌런 스폰 간격
    [SerializeField] private List<GameObject> villains = new List<GameObject>(); // 빌런 오브젝트 리스트

    [Header("클리어 및 사망 UI")]
    [SerializeField] private GameObject youSurvivedText; // "You Survived" 텍스트 오브젝트
    [SerializeField] private GameObject youDiedText; // "You Died" 텍스트 오브젝트
    [SerializeField] private GameObject clearImage; // 클리어 이미지

    public static GameManager Instance { get; private set; } // GameManager의 싱글톤 인스턴스

    private int currentVillainIndex = 0; // 현재 스폰될 빌런의 인덱스

    private void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 초기 UI 상태 설정
        youDiedText?.SetActive(false);
        youSurvivedText?.SetActive(false);
        clearImage?.SetActive(false);

        // 빌런 비활성화
        foreach (var villain in villains)
        {
            if (villain != null)
                villain.SetActive(false);
        }

        // 일정 시간 간격으로 빌런 스폰 시작
        InvokeRepeating(nameof(VillainsSpawn), startT, repeatT);
    }

    public void EndGame(bool clear)
    {
        if (clear)
        {
            youSurvivedText?.SetActive(true);
            clearImage?.SetActive(true);
        }
        else
        {
            youDiedText?.SetActive(true);
        }
    }

    public void VillainsSpawn()
    {
        // 현재 인덱스의 빌런 활성화
        if (currentVillainIndex < villains.Count && villains[currentVillainIndex] != null)
        {
            villains[currentVillainIndex].SetActive(true);
            Debug.Log($"{currentVillainIndex}번 빌런 등장!");
            currentVillainIndex++;
        }
        else
        {
            Debug.LogWarning("더 이상 스폰할 빌런이 없습니다.");
        }
    }
}
