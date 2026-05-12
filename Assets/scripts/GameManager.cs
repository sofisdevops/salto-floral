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
        // Al empezar, asegúrate de que el tiempo corra y el panel esté oculto
        Time.timeScale = 1f;
        if(gameOverPanel != null) gameOverPanel.SetActive(false);
        UpdateUI();
    }
    
    void Update() // "U" mayúscula
    {
        if(!isGameOver)
        {
            timer += Time.deltaTime; // Corregido "deltaTime"
            // Opcional: Actualizar un texto de tiempo en pantalla si tienes uno
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
        gameOverPanel.SetActive(true); // Corregido a "gameOverPanel"
        
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
            // Opcional: Podrías reiniciar la posición del jugador aquí 
            // en lugar de recargar toda la escena.
        }
    }

    // Esta función es para el BOTÓN de la interfaz
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
