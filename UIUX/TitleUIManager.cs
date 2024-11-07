using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    public GameObject gameManual;
    public GameObject credit;
    public Button Btn_play;
    public Button Btn_manual;
    public Button Btn_credit;
    public Button Btn_back;
    public string playScene;

    private void SetUIActive(GameObject uiElement, bool isActive)
    {
        uiElement.SetActive(isActive);
    }

    public void Start()
    {
        SetUIActive(gameManual, false);
        SetUIActive(credit, false);
        SetUIActive(Btn_back.gameObject, false);

        Btn_play.onClick.AddListener(() => SceneManager.LoadScene(playScene));
        Btn_manual.onClick.AddListener(() => {
            SetUIActive(gameManual, true);
            SetUIActive(Btn_back.gameObject, true);
        });
        Btn_credit.onClick.AddListener(() => {
            SetUIActive(credit, true);
            SetUIActive(Btn_back.gameObject, true);
        });
        Btn_back.onClick.AddListener(() => HideAllPanels());
    }

    public void HideAllPanels()
    {
        SetUIActive(gameManual, false);
        SetUIActive(credit, false);
        SetUIActive(Btn_back.gameObject, false);
    }
}
