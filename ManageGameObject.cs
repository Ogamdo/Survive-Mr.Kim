using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageGameObject : MonoBehaviour
{
    public GameObject objectToDelete;
    public GameObject objectToCreate;
    public Button firstButton;
    public Button secondButton;
    public string sceneToLoad; // 전환할 씬 이름

    void Start()
    {
        // 두 번째 버튼을 처음에는 비활성화
        secondButton.gameObject.SetActive(false);
        // 생성할 오브젝트를 처음에는 비활성화
        if (objectToCreate != null)
        {
            objectToCreate.SetActive(false);
        }

        // 첫 번째 버튼 클릭 이벤트에 메소드 등록
        firstButton.onClick.AddListener(OnFirstButtonClick);
        // 두 번째 버튼 클릭 이벤트에 메소드 등록
        secondButton.onClick.AddListener(OnSecondButtonClick);
    }

    // 첫 번째 버튼 클릭시 호출되는 메소드
    void OnFirstButtonClick()
    {
        if (objectToDelete != null)
        {
            Destroy(objectToDelete);
        }
        // 첫 번째 버튼 비활성화
        firstButton.gameObject.SetActive(false);
        // 두 번째 버튼 활성화
        secondButton.gameObject.SetActive(true);

        if (objectToCreate != null)
        {
            objectToCreate.SetActive(true);
        }
    }

    // 두 번째 버튼 클릭시 호출되는 메소드
    void OnSecondButtonClick()
    {
        // 두 번째 버튼 클릭 시 씬 전환
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
