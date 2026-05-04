using UnityEngine;
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

    public bool isGameOver = false;
    private float currentSpeed;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentSpeed = baseSpeed;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        currentSpeed = baseSpeed + gameTime * speedIncreaseRate;
        currentSpeed = Mathf.Clamp(currentSpeed, baseSpeed, maxSpeed);
        score += Mathf.RoundToInt(Time.deltaTime * 10);
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

        Time.timeScale = 0f;

        Debug.Log("GAME OVER");
    }

}
