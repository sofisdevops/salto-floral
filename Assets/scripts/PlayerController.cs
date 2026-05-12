using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 12f;
    public float jumpForce = 8f;
    private Rigidbody2D rb;
    private bool isGrounded;

    // 1. DECLARA LA VARIABLE (en minúsculas para diferenciarla de la clase)
    private GameManager gameManager; 
    private Animator animator;


    public Sprite spriteNormal; 
    public Sprite spriteIzquierda;

    // Referencia al componente que dibuja la imagen
    private SpriteRenderer spriteRenderer;

    public Sprite spriteSalto;
    public Sprite spriteCaida;

    void Start()
    {

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(spriteRenderer != null && spriteNormal != null){
            spriteRenderer.sprite = spriteNormal;
        }
        
        // 2. ASIGNA LA INSTANCIA A TU VARIABLE
        gameManager = Object.FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        animator.SetFloat("Velocidad",Mathf.Abs(moveInput));

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
        float xLimit = Mathf.Clamp(transform.position.x, -8.5f, 8.5f);
        transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);

        ActualizarAnimacion(moveInput);
    }

    void ActualizarAnimacion(float moveInput)
    {
        if (isGrounded)
        {
            // SI ESTÁ EN EL SUELO: Cambia entre normal e izquierda
            if (moveInput < 0) spriteRenderer.sprite = spriteIzquierda;
            else if (moveInput > 0) spriteRenderer.sprite = spriteNormal;
            // Si moveInput es 0, mantiene el último sprite puesto
        }
        else
        {
            // SI ESTÁ EN EL AIRE:
            if (rb.linearVelocity.y > 0.1f) 
            {
                spriteRenderer.sprite = spriteSalto; // Está subiendo
            }
            else if (rb.linearVelocity.y < -0.1f) 
            {
                spriteRenderer.sprite = spriteCaida; // Está bajando
            }
        }

        // Opcional: Voltear el sprite si quieres usar la misma pose de salto para ambos lados
        if (moveInput < 0) spriteRenderer.flipX = false; 
        else if (moveInput > 0) spriteRenderer.flipX = false; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        
        if (collision.gameObject.CompareTag("nuez"))
        {
            // 3. USA LA VARIABLE (en minúsculas)
            if (gameManager != null)
            {
                gameManager.ShowGameOver();
            }
            gameObject.SetActive(false);
        }
    }
}
