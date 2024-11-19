using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PlayUIManager : TitleUIManager
    {
        // Required buttons: Menu, Resume, Restart, Exit, Sound
        public Button Btn_menu;
        public Button Btn_resume;
       // public Button Btn_restart;
        public Button Btn_exit;
        public Button Btn_sound;

        // New UI elements for the pause panel
        public GameObject pausePanel; // Pause panel
        public GameObject playPanel; // Display clear condition checklist on the panel.
        public Slider soundSlider; // Sound adjustment slider

        // Timer related variables
        [HideInInspector]
        public GameTimer gameTimer;
       // private bool timeFlow = true;

        // Image movement related variables
        public RectTransform movingImage;
      //  public float ImageSlidingSpeed = 100f;

        void Start()
        {
            // Call Start from the parent class
            base.Start();

            // Initial state setup
            playPanel.SetActive(false); // Deactivate the play panel initially
            pausePanel.SetActive(false); // Deactivate the pause panel initially

            // Menu button to open the pause panel
            Btn_menu.onClick.AddListener(() => {
                pausePanel.SetActive(!pausePanel.activeSelf); // Toggle the visibility of the pause panel
                if (pausePanel.activeSelf)
                {
                    gameTimer.StartTimer(); // Pause the game timer operation
                }
                else
                {
                    gameTimer.StartTimer(); // Resume the game timer operation
                }
            });

            // Resume button to close the pause panel
            Btn_resume.onClick.AddListener(() => {
                pausePanel.SetActive(false);
                gameTimer.StartTimer(); // Resume the game timer
            });

            // Restart button to reload the current scene
          /*  Btn_restart.onClick.AddListener(() => {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Debug.Log("게임 재시작");
            }); */

            // Exit button to quit the application
            Btn_exit.onClick.AddListener(() => {
                Application.Quit();
                Debug.Log("게임 종료");
            });

            // Sound button to toggle sound on and off
            Btn_sound.onClick.AddListener(() => {
                AudioListener.volume = AudioListener.volume > 0 ? 0 : 1;
                soundSlider.value = AudioListener.volume; // Update the volume slider based on current volume
            });

            // Sound slider to adjust volume
            soundSlider.onValueChanged.AddListener(value => {
                AudioListener.volume = value;
                Debug.Log("사운드 볼륨 조절됨: " + value);
            });

            // Set initial timer text
            UpdateTimerText();
        }

        void Update()
        {
            // Update the timer text display
            if (gameTimer != null && gameTimer.IsGameActive())
            {
                UpdateTimerText();
            }

            // Handle the movement of the image
            // MoveImage();
        }

        // Update the timer display text
        void UpdateTimerText()
        {
            if (gameTimer != null)
            {
                int minutes = Mathf.FloorToInt(gameTimer.playTimeLimit / 60);
                int seconds = Mathf.FloorToInt(gameTimer.playTimeLimit % 60);
                gameTimer.timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }

        // Handle image movement
        // void MoveImage()
        // {
        //     // Move the image from right to left side
        //     float moveAmount = speed * Time.deltaTime;
        //     movingImage.anchoredPosition += new Vector2(-moveAmount, 0);
        // }
    }
}
