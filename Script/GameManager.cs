using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject youDiedText;
    public GameObject retryButton;

    private void Start()
    {
        // 초기 상태에서 UI 비활성화
        youDiedText.SetActive(false);
        retryButton.SetActive(false);
    }

    public void EndGame()
    {
        // 게임 종료 시 UI 활성화
        youDiedText.SetActive(true);
        retryButton.SetActive(true);
    }

    public void Retry()
    {
        // 특정 씬으로 이동
        SceneManager.LoadScene("SampleScene");
    }
}
