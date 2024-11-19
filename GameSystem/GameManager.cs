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
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ��� �ν��Ͻ� �ı�
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
