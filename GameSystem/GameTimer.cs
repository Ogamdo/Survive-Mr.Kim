using UnityEngine;
using System;

public class GameTimer : MonoBehaviour
{
    public float playTimeLimit = 60.0f;  // Game time limit in seconds
    private bool isGameActive = false;   // Indicates if the game is active
    private float currentTime;           // Holds the remaining time

    // Events to notify external classes of time updates and expiration
    public event Action<float> OnTimeUpdated;
    public event Action OnTimeExpired;

    void Start()
    {
        // Initialize the current time with the play time limit
        currentTime = playTimeLimit;
    }

    void Update()
    {
        // Check if the game is active
        if (isGameActive)
        {
            // Decrease time if there's still time left
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;  // Reduce time by deltaTime
                OnTimeUpdated?.Invoke(currentTime); // Trigger time update event
            }
            else
            {
                EndGame();  // End the game when time is up
            }
        }
    }

    void EndGame()
    {
        isGameActive = false;  // Set game active state to false
        Debug.Log("Game over! Time has expired.");  // Log the game end message

        OnTimeExpired?.Invoke();  // Trigger the time expired event

        GameManager.Instance.EndGame();  // Call GameManager to handle game end
    }

    public void RestartGame()
    {
        currentTime = playTimeLimit;  // Reset current time to the play time limit
        isGameActive = true;  // Set game active state to true
        OnTimeUpdated?.Invoke(currentTime);  // Trigger initial time update event
    }

    public void SetPlayTimeLimit(float newLimit)
    {
        playTimeLimit = newLimit;  // Set a new play time limit
        currentTime = newLimit;  // Reset current time to the new limit
        OnTimeUpdated?.Invoke(currentTime);  // Trigger event for new time limit
    }

    public bool IsGameActive()
    {
        return isGameActive;  // Return the active state of the game
    }

    public void StartTimer()
    {
        isGameActive = true;  // Activate the game timer
    }
}
