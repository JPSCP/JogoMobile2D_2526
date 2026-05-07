using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Speed")]
    public float baseSpeed = 8f;
    public float speedIncreaseRate = 0.2f;
    public float maxSpeed = 20f;

    [Header("Time")]
    public float gameTime = 0f;

    [Header("Score")]
    public int score = 0;

    [Header("UI")]
    public GameObject gameOverPanel;
    public GameObject pausePanel;

    [Header("Game States")]
    public bool isGameOver = false;

    private bool isPaused = false;

    private float currentSpeed;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentSpeed = baseSpeed;

        // Garante que os painéis começam desligados
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (isGameOver) return;

        // Tempo
        gameTime += Time.deltaTime;

        // Velocidade progressiva
        currentSpeed = baseSpeed + gameTime * speedIncreaseRate;
        currentSpeed = Mathf.Clamp(currentSpeed, baseSpeed, maxSpeed);

        // Score por tempo
        score += Mathf.RoundToInt(Time.deltaTime * 10);

        // ESC = Pause
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    public float GetSpeed()
    {
        return currentSpeed;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;

        Debug.Log("GAME OVER");
    }


    public void PauseGame()
    {
        if (isGameOver) return;

        if (pausePanel != null)
            pausePanel.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
