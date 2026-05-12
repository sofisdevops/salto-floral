using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lives = 3;
    public int totalScore = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalTimeText;

    private float timer;
    private bool isGameOver = false;

    void Start()
    {
        Time.timeScale = 1f;
        if(gameOverPanel != null) gameOverPanel.SetActive(false);
        UpdateUI();
    }
    
    void Update()
    {
        if(!isGameOver)
        {
            timer += Time.deltaTime;
        }    
    }

    public void AddScore(int points)
    {
        totalScore += points;
        UpdateUI();
    }

    public void ShowGameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true); 
        
        finalScoreText.text = "Score Total: " + totalScore.ToString();
        finalTimeText.text = "Tiempo: " + Mathf.FloorToInt(timer).ToString() + "s";
        
        Time.timeScale = 0f; // Pausa el juego
    }

    public void TakeDamage()
    {
        lives--;
        UpdateUI();

        if (lives <= 0) 
        {
            ShowGameOver();
        }
        else
        {
            // Opcional:  reiniciar la posición del jugador aquí 
            // en lugar de recargar toda la escena.
        }
    }

    public void RestartGameButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateUI()
    {
        if(scoreText != null) scoreText.text = "Score: " + totalScore;
        if(livesText != null) livesText.text = "Lives: " + lives;
    }
}
