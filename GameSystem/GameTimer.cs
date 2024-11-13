using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float playTimeLimit = 60.0f;  // κ²μ ?κ°? (μ΄? ?¨?)
    private bool isGameActive = false;  // μ΄κΈ° ??λ₯? falseλ‘? λ³?κ²?
    private GameManager gameManager;
    public Text timerText;  // UI Text κ°μ²΄λ₯? μ°Έμ‘°

    void Start()
    {
        // GameManager κ°μ²΄λ₯? μ°Ύμ΅??€.
        gameManager = FindObjectOfType<GameManager>();
        UpdateTimerUI(); // μ΄κΈ°? ? ?κ°μ ???©??€.
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(playTimeLimit).ToString();
        }
    }

    void Update()
    {
        if (isGameActive)
        {
            if (playTimeLimit > 0)
            {
                playTimeLimit -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                EndGame();
            }
        }
    }

    void EndGame()
    {
        isGameActive = false;
        Debug.Log("κ²μ μ’λ£! ?κ°μ΄ μ΄κ³Ό???΅??€.");

        // GameManager? EndGame λ©μ?λ₯? ?ΈμΆν?¬ UIλ₯? λΉν?±??©??€.
        if (gameManager != null)
        {
            gameManager.EndGame();
        }
    }

    public void RestartGame()
    {
        playTimeLimit = 60.0f; // κ²μ ?κ°μ ?€? μ΄κΈ°?
        isGameActive = true;
        UpdateTimerUI();  // UI ??°?΄?Έ
    }

    public void SetPlayTimeLimit(float newLimit)
    {
        playTimeLimit = newLimit;
        UpdateTimerUI();  // ? ?κ°? ? ?? ?€? ?κ³? UI ??°?΄?Έ
    }

    public bool IsGameActive()
    {
        return isGameActive;
    }

    public void StartTimer()
    {
        isGameActive = true;
    }
}
