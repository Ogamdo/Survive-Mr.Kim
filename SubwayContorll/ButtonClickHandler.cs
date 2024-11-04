using UnityEngine;
using UnityEngine.UI;

public class ButtonClickHandler : MonoBehaviour
{
    public Button myButton;
    public GameObject targetObject; // 활성화할 게임 오브젝트

    void Start()
    {
        // 버튼이 null인지 확인한 후 클릭 이벤트 추가
        if (myButton != null)
        {
            myButton.onClick.AddListener(OnButtonClick);
        }
    }

    // 버튼 클릭시 호출될 함수
    void OnButtonClick()
    {
        Debug.Log("버튼이 클릭되었습니다!");
        if (targetObject != null)
        {
            targetObject.SetActive(true); // 게임 오브젝트 활성화
        }
    }
}
