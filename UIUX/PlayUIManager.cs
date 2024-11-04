using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUIManager : TitleUIManager
{
    // 단순 회색 창
    public GameObject grayPanel;
    
    // 체크박스 버튼 세 개
    public Toggle checkbox1;
    public Toggle checkbox2;
    public Toggle checkbox3;

    void Start()
    {
        // 초기화 및 이벤트 추가
        checkbox1.onValueChanged.AddListener(OnCheckboxChanged);
        checkbox2.onValueChanged.AddListener(OnCheckboxChanged);
        checkbox3.onValueChanged.AddListener(OnCheckboxChanged);

        // 창을 초기 상태로 설정 (예: 비활성화)
        grayPanel.SetActive(false); // 초기에는 창을 비활성화
    }

    void Update()
    {
        // UI 업데이트 로직 (필요시 추가)
    }

    private void OnCheckboxChanged(bool isChecked)
    {
        Toggle sender = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>();
        
        if (sender == checkbox1)
        {
            // 체크박스 1의 상태에 따른 행동 정의
            Debug.Log("Checkbox 1: " + isChecked);
        }
        else if (sender == checkbox2)
        {
            // 체크박스 2의 상태에 따른 행동 정의
            Debug.Log("Checkbox 2: " + isChecked);
        }
        else if (sender == checkbox3)
        {
            // 체크박스 3의 상태에 따른 행동 정의
            Debug.Log("Checkbox 3: " + isChecked);
        }
    }
}