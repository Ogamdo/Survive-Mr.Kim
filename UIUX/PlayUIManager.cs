using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PlayUIManager : TitleUIManager
    {
        // �ʿ��� ��ư: �޴�, �簳, �����, ����, ����
        public Button Btn_menu;
        public Button Btn_resume;
        public Button Btn_restart;
        public Button Btn_exit;
        public Button Btn_sound;

        // �Ͻ����� �г� UI ��ҵ�
        public GameObject pausePanel; // �Ͻ����� �г�
        public GameObject playPanel; // Ŭ���� ���� üũ����Ʈ �г�
        public Slider soundSlider; // ���� ���� �����̴�

        // Ÿ�̸� ���� ����
        [HideInInspector]
        public GameTimer gameTimer;
        private bool timeFlow = true;

        // �̹��� �̵� ���� ����
        public RectTransform movingImage;
        // public float ImageSlidingSpeed = 100f;

        void Start()
        {
            // �θ� Ŭ������ Start ȣ��
            base.Start();

            // �ʱ� ���� ����
            playPanel.SetActive(false); // �÷��� �г� ��Ȱ��ȭ
            pausePanel.SetActive(false); // �Ͻ����� �г� ��Ȱ��ȭ

            // �޴� ��ư: �Ͻ����� �г� ǥ��/����
            Btn_menu.onClick.AddListener(() => {
                pausePanel.SetActive(!pausePanel.activeSelf);
                if (pausePanel.activeSelf)
                {
                    gameTimer.StartTimer(); // Ÿ�̸� �Ͻ�����
                }
                else
                {
                    gameTimer.StartTimer(); // Ÿ�̸� �簳
                }
            });

            // �簳 ��ư: �Ͻ����� �г� �ݰ� ���� �簳
            Btn_resume.onClick.AddListener(() => {
                pausePanel.SetActive(false);
                gameTimer.StartTimer(); // Ÿ�̸� �簳
            });

            // ����� ��ư: ���� �� �����
            Btn_restart.onClick.AddListener(() => {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Debug.Log("���� �����");
            });

            // ���� ��ư: ���� ����
            Btn_exit.onClick.AddListener(() => {
                Application.Quit();
                Debug.Log("���� ����");
            });

            // ���� ��ư: ���� �Ѱ� ����
            Btn_sound.onClick.AddListener(() => {
                AudioListener.volume = AudioListener.volume > 0 ? 0 : 1;
                soundSlider.value = AudioListener.volume; // ���� ������ ���� �����̴� ������Ʈ
            });

            // ���� �����̴�: ���� ����
            soundSlider.onValueChanged.AddListener(value => {
                AudioListener.volume = value;
                Debug.Log("���� ���� ������: " + value);
            });

            // �ʱ� Ÿ�̸� �ؽ�Ʈ ����
            UpdateTimerText();
        }

        void Update()
        {
            // Ÿ�̸Ӱ� Ȱ�� ������ ��� Ÿ�̸� UI ������Ʈ
            if (gameTimer != null && gameTimer.IsGameActive())
            {
                UpdateTimerText();
            }

            // �̹��� �̵� ó�� (�ּ� ó���� ���� �޼���)
            // MoveImage();
        }

        // Ÿ�̸� UI �ؽ�Ʈ�� 120�� �������� ������Ʈ
        void UpdateTimerText()
        {
            if (gameTimer != null)
            {
                // ���� �ð��� 120�� �������� ���
                float remainingTime = Mathf.Clamp(120 - gameTimer.playTimeLimit, 0, 120);
                int minutes = Mathf.FloorToInt(remainingTime / 60);
                int seconds = Mathf.FloorToInt(remainingTime % 60);

                gameTimer.timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }

        // �̹��� �̵� ó�� (�ּ� ó���� ���� �޼���)
        // void MoveImage()
        // {
        //     float moveAmount = ImageSlidingSpeed * Time.deltaTime;
        //     movingImage.anchoredPosition += new Vector2(-moveAmount, 0);
        // }
    }
}
