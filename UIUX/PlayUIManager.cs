using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayUIManager : TitleUIManager
{
    // Required buttons: Resume, Restart, Exit, Sound
    public Button Btn_resume;
    public Button Btn_restart;
    public Button Btn_exit;
    public Button Btn_sound;

    // Simple gray panel
    public GameObject grayPanel;
    
    // Three checkbox buttons, may need to be accessed by other classes, usually related to specific missions
    public Toggle checkbox1;
    public Toggle checkbox2;
    public Toggle checkbox3;

    // Timer related variables
    public Text timerText;
    private float time = 120f;
    private bool timerRunning = true;

    // Image movement related variables
    public RectTransform movingImage;
    public float speed = 100f;

    void Start()
    {
        // Call Start from the parent class
        base.Start();

        // Initial state setup
        grayPanel.SetActive(false); // Initially deactivate the gray panel

        // Set button click listeners (using lambda expressions)
        Btn_resume.onClick.AddListener(() => {
            grayPanel.SetActive(false);
            Debug.Log("게임 이어하기");
        });
        Btn_restart.onClick.AddListener(() => {
    base.OnClickButton("play");
    Debug.Log("게임 다시 시작");
});
        Btn_exit.onClick.AddListener(() => {
            Application.Quit();
            Debug.Log("게임 종료");
        });
        Btn_sound.onClick.AddListener(() => {
            Debug.Log("사운드 설정 버튼 클릭됨");
            AudioListener.volume = AudioListener.volume > 0 ? 0 : 1;
        });

        // Add checkbox events (using lambda expressions)
        checkbox1.onValueChanged.AddListener(isChecked => Debug.Log("Checkbox 1: " + isChecked));
        checkbox2.onValueChanged.AddListener(isChecked => Debug.Log("Checkbox 2: " + isChecked));
        checkbox3.onValueChanged.AddListener(isChecked => Debug.Log("Checkbox 3: " + isChecked));

        // Set initial timer text
        UpdateTimerText();
    }

    void Update()
    {
        // Decrease time if the timer is running
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                timeRemaining = 0;
                timerRunning = false;
                UpdateTimerText();
                Debug.Log("Time's up!");
            }
        }

        // Handle image movement
        MoveImage();
    }

    // Update timer text
    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // 이미지 이동 처리
    void MoveImage()
    {
        // Move the image from right to left
        float moveAmount = speed * Time.deltaTime;
        movingImage.anchoredPosition += new Vector2(-moveAmount, 0);
    }
}
