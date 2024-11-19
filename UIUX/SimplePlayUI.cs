using UnityEngine;
using UnityEngine.UI;

public class SimplePlayUI : MonoBehaviour
{
    // UI ���: �Ͻ�����, �簳, ���� �����̴�
    public Button btnPause; // �Ͻ����� ��ư
    public Button btnResume; // �簳 ��ư
    public Slider volumeSlider; // ���� ���� �����̴�
 public GameTimer gameTimer; // GameTimer ��ü�� �����Ϳ��� �Ҵ�

    private bool isPaused = false; // �Ͻ����� ���� Ȯ�ο�

    void Start()
    {   
        // UI �ʱ� ����
        btnResume.gameObject.SetActive(false); // ó������ �簳 ��ư ��Ȱ��ȭ
        volumeSlider.gameObject.SetActive(false); // ó������ ���� �����̴� ��Ȱ��ȭ
        volumeSlider.value = AudioListener.volume; // ���� ������ ���� �����̴� ����

        // ��ư �� �����̴��� �̺�Ʈ ������ �߰�
        btnPause.onClick.AddListener(PauseGame);
        btnResume.onClick.AddListener(ResumeGame);
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // Ÿ�̸� UI�� 1�ʸ��� ������Ʈ�ϵ��� ����
        if (gameTimer != null)
        {
            InvokeRepeating("UpdateTimerUI", 0f, 1f); // 1�� �������� UI ������Ʈ
        }
    }

    void UpdateTimerUI()
    {
        if (!isPaused && gameTimer != null && gameTimer.IsGameActive())
        {
            gameTimer.UpdateTimerUI(); // Ÿ�̸� UI�� 1�ʸ��� ������Ʈ
        }
    }

    // ���� �Ͻ�����
    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; // ���� �ð��� ����
        gameTimer.PauseTimer(); // Ÿ�̸� ����
        btnPause.gameObject.SetActive(false); // �Ͻ����� ��ư ����
        btnResume.gameObject.SetActive(true); // �簳 ��ư ǥ��
        volumeSlider.gameObject.SetActive(true); // ���� �����̴� ǥ��
        Debug.Log("���� �Ͻ�����");
    }

    // ���� �簳
    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; // ���� �ð��� ���� �ӵ��� �簳
        gameTimer.StartTimer(); // Ÿ�̸� �簳
        btnPause.gameObject.SetActive(true); // �Ͻ����� ��ư ǥ��
        btnResume.gameObject.SetActive(false); // �簳 ��ư ����
        volumeSlider.gameObject.SetActive(false); // ���� �����̴� ����
        Debug.Log("���� �簳");
    }

    // ���� ���� ����
    void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        Debug.Log("���� ���� ������: " + volume);
    }

    void OnDestroy()
    {
        // �� ��ũ��Ʈ�� �ı��� �� InvokeRepeating ����
        CancelInvoke("UpdateTimerUI");
    }
}
