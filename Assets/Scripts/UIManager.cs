using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (GameManager.Instance == null) return;

        UpdateUI();
    }

    void UpdateUI()
    {
        // Score
        scoreText.text = "Score: " + GameManager.Instance.score.ToString("00000");

        // Time (formatado)
        int seconds = Mathf.FloorToInt(GameManager.Instance.gameTime);
        timeText.text = "Time: " + seconds + "s";
    }
}
