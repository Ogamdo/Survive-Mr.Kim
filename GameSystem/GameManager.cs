using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject youSurvivedText;
    public GameObject youDiedText;
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
        youSurvivedText.SetActive(false);
    }

    public void EndGame()
    {
        youDiedText.SetActive(true);
    }
    public void ClearGame()
    {
        youSurvivedText.SetActive(!false);
    }
}
