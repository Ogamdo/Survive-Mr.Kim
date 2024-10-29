using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageGameObject : MonoBehaviour
{
    public GameObject objectToDelete;
    public GameObject objectToCreate;
    public Button firstButton;
    public Button secondButton;
    public string sceneToLoad; // ��ȯ�� �� �̸�

    void Start()
    {
        // �� ��° ��ư�� ó������ ��Ȱ��ȭ
        secondButton.gameObject.SetActive(false);
        // ������ ������Ʈ�� ó������ ��Ȱ��ȭ
        if (objectToCreate != null)
        {
            objectToCreate.SetActive(false);
        }

        // ù ��° ��ư Ŭ�� �̺�Ʈ�� �޼ҵ� ���
        firstButton.onClick.AddListener(OnFirstButtonClick);
        // �� ��° ��ư Ŭ�� �̺�Ʈ�� �޼ҵ� ���
        secondButton.onClick.AddListener(OnSecondButtonClick);
    }

    // ù ��° ��ư Ŭ���� ȣ��Ǵ� �޼ҵ�
    void OnFirstButtonClick()
    {
        if (objectToDelete != null)
        {
            Destroy(objectToDelete);
        }
        // ù ��° ��ư ��Ȱ��ȭ
        firstButton.gameObject.SetActive(false);
        // �� ��° ��ư Ȱ��ȭ
        secondButton.gameObject.SetActive(true);

        if (objectToCreate != null)
        {
            objectToCreate.SetActive(true);
        }
    }

    // �� ��° ��ư Ŭ���� ȣ��Ǵ� �޼ҵ�
    void OnSecondButtonClick()
    {
        // �� ��° ��ư Ŭ�� �� �� ��ȯ
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
