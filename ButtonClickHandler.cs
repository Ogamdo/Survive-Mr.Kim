using UnityEngine;
using UnityEngine.UI;

public class ButtonClickHandler : MonoBehaviour
{
    public Button myButton;
    public GameObject targetObject; // Ȱ��ȭ�� ���� ������Ʈ

    void Start()
    {
        // ��ư�� null���� Ȯ���� �� Ŭ�� �̺�Ʈ �߰�
        if (myButton != null)
        {
            myButton.onClick.AddListener(OnButtonClick);
        }
    }

    // ��ư Ŭ���� ȣ��� �Լ�
    void OnButtonClick()
    {
        Debug.Log("��ư�� Ŭ���Ǿ����ϴ�!");
        if (targetObject != null)
        {
            targetObject.SetActive(true); // ���� ������Ʈ Ȱ��ȭ
        }
    }
}
