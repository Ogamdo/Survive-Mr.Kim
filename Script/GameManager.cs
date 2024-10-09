using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject youDiedText;
    public GameObject retryButton;


    private void Start()
    {
        youDiedText.SetActive(false);
        retryButton.SetActive(false);
    }

    public void EndGame()
    {
        youDiedText.SetActive(true);
        retryButton.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene("Intro");
    }

}
