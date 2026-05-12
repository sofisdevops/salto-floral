using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 12f;
    public float jumpForce = 8f;
    private Rigidbody2D rb;
    private bool isGrounded;

    private GameManager gameManager; 
    private Animator animator;


    public Sprite spriteNormal; 
    public Sprite spriteIzquierda;

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
            if (moveInput < 0) spriteRenderer.sprite = spriteIzquierda;
            else if (moveInput > 0) spriteRenderer.sprite = spriteNormal;
        }
        else
        {
            if (rb.linearVelocity.y > 0.1f) 
            {
                spriteRenderer.sprite = spriteSalto;
            }
            else if (rb.linearVelocity.y < -0.1f) 
            {
                spriteRenderer.sprite = spriteCaida;
            }
        }

        if (moveInput < 0) spriteRenderer.flipX = true; 
        else if (moveInput > 0) spriteRenderer.flipX = true; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        
        if (collision.gameObject.CompareTag("nuez"))
        {
            if (gameManager != null)
            {
                gameManager.ShowGameOver();
            }
            gameObject.SetActive(false);
        }
    }
}
