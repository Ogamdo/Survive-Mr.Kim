using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject youDiedText;
    public GameObject retryButton;
  public static GameManager Instance { get; private set; }
   private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 파괴
        }
    }
   void Start()
    {
        youDiedText.SetActive(false);
        retryButton.SetActive(false);
    }

    public void EndGame()
    {
        youDiedText.SetActive(true);
        retryButton.SetActive(true);
    }

 

}
