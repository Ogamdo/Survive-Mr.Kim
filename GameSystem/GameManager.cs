using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("빌런 설정")]
    [SerializeField] public float startT = 15f; // 빌런 스폰 시작 시간
    [SerializeField] public float repeatT = 15f; // 빌런 스폰 간격
    [SerializeField] private List<GameObject> villains = new List<GameObject>(); // 빌런 오브젝트 리스트

    public static GameManager Instance { get; private set; } // 싱글톤 인스턴스

    private int currentVillainIndex = 0;

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
        // 빌런 비활성화
        foreach (var villain in villains)
        {
            if (villain != null)
                villain.SetActive(false);
        }

        // 빌런 스폰 시작
        InvokeRepeating(nameof(VillainsSpawn), startT, repeatT);
    }

    public void VillainsSpawn()
    {
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
