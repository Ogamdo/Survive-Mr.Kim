using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    public GameObject gameManual;
    public GameObject credits;
    public Button btnPlay;
    public Button btnManual;
    public Button btnCredit;
    public Button btnBack;
    public string playScene;

    private void SetUIActive(GameObject uiElement, bool isActive)
    {
        uiElement.SetActive(isActive);
    }

    public void Start()
    {
        SetUIActive(gameManual, false);
        SetUIActive(credits, false);
        SetUIActive(btnBack.gameObject, false);

        btnPlay.onClick.AddListener(() => SceneManager.LoadScene(playScene));
        btnManual.onClick.AddListener(() => {
            SetUIActive(gameManual, true);
            SetUIActive(btnBack.gameObject, true);
        });
        btnCredit.onClick.AddListener(() => {
            SetUIActive(credits, true);
            SetUIActive(btnBack.gameObject, true);
        });
        btnBack.onClick.AddListener(() => HideAllPanels());
    }

    public void HideAllPanels()
    {
        SetUIActive(gameManual, false);
        SetUIActive(credits, false);
        SetUIActive(btnBack.gameObject, false);
    }
}
