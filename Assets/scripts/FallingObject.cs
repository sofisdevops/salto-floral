using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [Header("Configuracion de Objeto")]
    public int scoreValue = 1;
    public bool nuez = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager gm = Object.FindFirstObjectByType<GameManager>();

            if (nuez)
            {
                gm.TakeDamage();
            }
            else
            {
                gm.AddScore(scoreValue);
            }

            Destroy(gameObject);
        }

        if (collision.CompareTag("Ground") || collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}