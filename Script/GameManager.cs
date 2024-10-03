using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public GameObject youDiedText;
    public GameObject retryButton;

    private void Start()
    {
        // �ʱ� ���¿��� UI ��Ȱ��ȭ
        youDiedText.SetActive(false);
        retryButton.SetActive(false);
    }

    public void EndGame()
    {
        // ���� ���� �� UI Ȱ��ȭ
        youDiedText.SetActive(true);
        retryButton.SetActive(true);
    }

    public void Retry()
    {
        // Ư�� ������ �̵�
        SceneManager.LoadScene("SampleScene");
    }
}
