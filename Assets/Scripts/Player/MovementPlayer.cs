// --- LIBRERÍAS LIMPIAS ---
using UnityEngine;
using System.Collections;

// No necesitas la línea de "Zarek..." aquí, es solo un comentario.
public class MovementPlayer : MonoBehaviour
{
    // --- VARIABLES DEL JUGADOR ---
    [Header("PLAYER STATS")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 15.0f;
    [SerializeField] private float downardForce = 25.0f; 

    // --- COMPONENTES DEL OBJETO
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // --- VARIABLES DE ESTADO ---
    private float horizontalInput;
    private bool isGrounded = false;
    private bool isKnockedBack = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Esto nos da un valor entre -1 (izquierda total) y 1 (derecha total).
        horizontalInput = Input.GetAxis("Horizontal");

        FlipSprite();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false; 
        }

        // Usamos la variable que SÍ tiene el valor del input.
        animator.SetFloat("speed", Mathf.Abs(horizontalInput));
    }

    private void FixedUpdate()
    {
        // Aplicamos movimiento con linearVelocity si no estamos siendo empujados
        if (!isKnockedBack)
        {
            rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
        }

        // Aplicamos una fuerza extra hacia abajo si el personaje está cayendo, para un salto más "pesado".
        if (rb.linearVelocity.y < 0)
        {
            rb.AddForce(Vector2.down * downardForce * Time.fixedDeltaTime, ForceMode2D.Force);
        }
    }

    // FUNCIÓN PÚBLICA PARA RECIBIR EL KNOCKBACK ---
    public void ApplyKnockback(Vector2 direction, float force)
    {
        StartCoroutine(KnockbackCoroutine(direction, force));
    }

    private IEnumerator KnockbackCoroutine(Vector2 direction, float force)
    {
        isKnockedBack = true; // Desactivamos el control del jugador
        rb.linearVelocity = Vector2.zero; // Reseteamos la velocidad
        rb.AddForce(direction * force, ForceMode2D.Impulse); // Aplicamos el impulso

        yield return new WaitForSeconds(0.2f); // Esperamos una fracción de segundo

        isKnockedBack = false; // Devolvemos el control al jugador
    }

    private void FlipSprite()
    {
        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true; // Giramos el sprite a la izquierda
        }
        else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false; // Lo ponemos en su dirección default ( derecha )
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}