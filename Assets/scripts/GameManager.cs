using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lives = 3;
    public int totalScore = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int points)
    {
        totalScore += points;
        UpdateUI();
    }

    public void TakeDamage()
    {
        lives--;
        UpdateUI();
        if (lives <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateUI()
    {
        scoreText.text = "" + totalScore;
        livesText.text = "" + lives;
    }
}