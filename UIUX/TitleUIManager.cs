
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
    public GameObject Btn_back;
    public string play1;

    public void Start()
    {
        gameManual.SetActive(false);
        credit.SetActive(false);
        Btn_back.SetActive(false);
    }

    public void OnClickButton(string action)
    {
        switch (action)
        {
            case "play":
                SceneManager.LoadScene(play1);
                break;
            case "manual":
                ToggleUI(gameManual, true);
                break;
            case "credit":
                ToggleUI(credit, true);
                break;
            case "back":
                ToggleUI(gameManual, false);
                ToggleUI(credit, false);
                break;
        }
    }

    private void ToggleUI(GameObject uiElement, bool isActive)
    {
        uiElement.SetActive(isActive);
        Btn_back.SetActive(isActive); // 뒤로 가기 버튼 상태를 UI 요소와 동기화
    }
}