using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Stats")]
    public int score = 0;
    public int lives = 3;

    [Header("UI")]
    public Text scoreText;
    public Text livesText;
    public GameObject gameOverPanel;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void LoseLife()
    {
        lives--;
        UpdateUI();
        if (lives <= 0)
            GameOver();
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ✅ TAMBAHAN BARU
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene"); // sesuaikan nama scene kamu
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }
}