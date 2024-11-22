using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PlayUIManager : TitleUIManager
    {
        // 필요한 버튼: 메뉴, 재개, 재시작, 종료, 사운드
        public Button Btn_menu;
        public Button Btn_resume;
        public Button Btn_restart;
        public Button Btn_exit;
        public Button Btn_sound;

        // 일시정지 패널 UI 요소들
        public GameObject pausePanel; // 일시정지 패널
        public GameObject playPanel; // 클리어 조건 체크리스트 패널
        public Slider soundSlider; // 사운드 조절 슬라이더

        // 타이머 관련 변수
        [HideInInspector]
        public GameTimer gameTimer;
        private bool timeFlow = true;

        // 이미지 이동 관련 변수
        public RectTransform movingImage;
        // public float ImageSlidingSpeed = 100f;

        void Start()
        {
            // 부모 클래스의 Start 호출
            base.Start();

            // 초기 상태 설정
            playPanel.SetActive(false); // 플레이 패널 비활성화
            pausePanel.SetActive(false); // 일시정지 패널 비활성화

            // 메뉴 버튼: 일시정지 패널 표시/숨김
            Btn_menu.onClick.AddListener(() => {
                pausePanel.SetActive(!pausePanel.activeSelf);
                if (pausePanel.activeSelf)
                {
                    gameTimer.StartTimer(); // 타이머 일시정지
                }
                else
                {
                    gameTimer.StartTimer(); // 타이머 재개
                }
            });

            // 재개 버튼: 일시정지 패널 닫고 게임 재개
            Btn_resume.onClick.AddListener(() => {
                pausePanel.SetActive(false);
                gameTimer.StartTimer(); // 타이머 재개
            });

            // 재시작 버튼: 현재 씬 재시작
            Btn_restart.onClick.AddListener(() => {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Debug.Log("게임 재시작");
            });

            // 종료 버튼: 게임 종료
            Btn_exit.onClick.AddListener(() => {
                Application.Quit();
                Debug.Log("게임 종료");
            });

            // 사운드 버튼: 사운드 켜고 끄기
            Btn_sound.onClick.AddListener(() => {
                AudioListener.volume = AudioListener.volume > 0 ? 0 : 1;
                soundSlider.value = AudioListener.volume; // 현재 볼륨에 맞춰 슬라이더 업데이트
            });

            // 사운드 슬라이더: 볼륨 조절
            soundSlider.onValueChanged.AddListener(value => {
                AudioListener.volume = value;
                Debug.Log("사운드 볼륨 조절됨: " + value);
            });

            // 초기 타이머 텍스트 설정
            UpdateTimerText();
        }

        void Update()
        {
            // 타이머가 활성 상태일 경우 타이머 UI 업데이트
            if (gameTimer != null && gameTimer.IsGameActive())
            {
                UpdateTimerText();
            }

            // 이미지 이동 처리 (주석 처리된 원본 메서드)
            // MoveImage();
        }

        // 타이머 UI 텍스트를 120초 기준으로 업데이트
        void UpdateTimerText()
        {
            if (gameTimer != null)
            {
                // 남은 시간을 120초 기준으로 계산
                float remainingTime = Mathf.Clamp(120 - gameTimer.playTimeLimit, 0, 120);
                int minutes = Mathf.FloorToInt(remainingTime / 60);
                int seconds = Mathf.FloorToInt(remainingTime % 60);

                gameTimer.timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }

        // 이미지 이동 처리 (주석 처리된 원본 메서드)
        // void MoveImage()
        // {
        //     float moveAmount = ImageSlidingSpeed * Time.deltaTime;
        //     movingImage.anchoredPosition += new Vector2(-moveAmount, 0);
        // }
    }
}
