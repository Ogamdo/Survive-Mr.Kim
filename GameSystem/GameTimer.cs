using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float playTimeLimit = 60.0f;  // ê²Œì„ ?‹œê°? (ì´? ?‹¨?œ„)
    private bool isGameActive = false;  // ì´ˆê¸° ?ƒ?ƒœë¥? falseë¡? ë³?ê²?
    private GameManager gameManager;
    public Text timerText;  // UI Text ê°ì²´ë¥? ì°¸ì¡°

    void Start()
    {
        // GameManager ê°ì²´ë¥? ì°¾ìŠµ?‹ˆ?‹¤.
        gameManager = FindObjectOfType<GameManager>();
        UpdateTimerUI(); // ì´ˆê¸°?™” ?‹œ ?‹œê°„ì„ ?‘œ?‹œ?•©?‹ˆ?‹¤.
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
        Debug.Log("ê²Œì„ ì¢…ë£Œ! ?‹œê°„ì´ ì´ˆê³¼?˜?—ˆ?Šµ?‹ˆ?‹¤.");

        // GameManager?˜ EndGame ë©”ì„œ?“œë¥? ?˜¸ì¶œí•˜?—¬ UIë¥? ë¹„í™œ?„±?™”?•©?‹ˆ?‹¤.
        if (gameManager != null)
        {
            gameManager.EndGame();
        }
    }

    public void RestartGame()
    {
        playTimeLimit = 60.0f; // ê²Œì„ ?‹œê°„ì„ ?‹¤?‹œ ì´ˆê¸°?™”
        isGameActive = true;
        UpdateTimerUI();  // UI ?—…?°?´?Š¸
    }

    public void SetPlayTimeLimit(float newLimit)
    {
        playTimeLimit = newLimit;
        UpdateTimerUI();  // ?ƒˆ ?‹œê°? ? œ?•œ?„ ?„¤? •?•˜ê³? UI ?—…?°?´?Š¸
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
