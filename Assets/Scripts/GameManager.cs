using UnityEngine;
using UnityEngine.UI;
using TMPro; // Ensure you have the TextMeshPro package installed if you want to use TMP instead of Unity's default Text

public class GameManager : MonoBehaviour
{

    public static GameManager Instance; // Singleton instance of the GameManager

    public TextMeshProUGUI scoreText; // UI Text element to display the score
    public TextMeshProUGUI timerText; // UI Text element to display the timer
    public TextMeshProUGUI levelText; // UI Text element to display the level
    public GameObject gameOverPanel; // UI Panel to display when the game is over
    public GameObject levelCompletePanel; // UI Panel to display when the level is complete
    public Transform basketTransform; // Transform of the basket for positioning

    public int targerScore = 10; // Target score to complete the level
    public float levelTime = 60f; // Time limit for the level in seconds

    private int currentScore = 0; // Current score of the player
    private float timer; // Timer for the level
    private int currentLevel = 1; // Current level of the game
    private bool gameActive = true; // Flag to check if the game is active

    void Awake()
    {
        if (Instance == null) Instance = this; // Set the singleton instance
        else Destroy(gameObject); // Destroy duplicate instances
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = levelTime; // Initialize the timer with the level time
        UpdateUI(); // Update the UI elements with initial values
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameActive) return; // If the game is not active, skip the update

        timer -= Time.deltaTime; // Decrease the timer by the time elapsed since the last frame
        timerText.text = "Time: " + Mathf.Ceil(timer);

        if (timer <= 0) // Check if the timer has reached zero
        {
            GameOver(); // Call the GameOver method
        }
    }

    public void AddScore(int score)
    {
        if (!gameActive) return; // If the game is not active, skip adding score

        currentScore += score; // Increase the current score by the given score
        UpdateUI(); // Update the UI elements with the new score

        if (currentScore >= targerScore) // Check if the current score has reached or exceeded the target score
        {
            LevelComplete(); // Call the LevelComplete method
        }
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + currentScore; // Update the score text
        levelText.text = "Level: " + currentLevel; // Update the level text
    }

    void GameOver()
    {
        gameActive = false; // Set the game as inactive
        gameOverPanel.SetActive(true); // Activate the Game Over panel
        Debug.Log("Game Over!"); // Log a message to the console
    }

    void LevelComplete()
    {
        gameActive = false; // Set the game as inactive
        levelCompletePanel.SetActive(true); // Activate the Level Complete panel
        Debug.Log("Level Complete!"); // Log a message to the console

        if (currentLevel == 1)
            StartCoroutine(MoveBasketUpDown()); // If it's the first level, load the next level

        Invoke("NextLevel", 3f); // Wait for 3 seconds before moving to the next level

    }

    void NextLevel()
    {
        currentLevel++; // Increase the current level
        currentScore = 0; // Reset the current score
        timer = levelTime; // Reset the timer to the level time
        UpdateUI(); // Update the UI elements with the new values
        gameActive = true; // Set the game as active
        levelCompletePanel.SetActive(false); // Deactivate the Level Complete panel
        Debug.Log("Loading Level " + currentLevel); // Log a message to the console
    }

    System.Collections.IEnumerator MoveBasketUpDown()
    {
        while (true)
        {
            if (!gameActive) yield break;

            basketTransform.position = new Vector3(
                basketTransform.position.x,
                Mathf.PingPong(Time.time * 2f, 3f) + 1f,
                basketTransform.position.z
            );

            yield return null;
        } 
    }

}
