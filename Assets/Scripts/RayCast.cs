using System.Runtime.CompilerServices;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 1.5f;

    [Header("Detección de Entorno")]
    [SerializeField] private Transform wallCheckPoint; // Un punto para lanzar el rayo de pared
    [SerializeField] private Transform ledgeCheckPoint; // Un punto para lanzar el rayo de abismo
    [SerializeField] private float checkRadius = 0.1f; // El radio de detección
    [SerializeField] private LayerMask whatIsGround; // La capa del suelo/paredes

    [Header("Combate")]
    [SerializeField] private int contactDamage = 1;

    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private Animator animator;

    // VARIABLES PARA SISTEMA DE ESTADOS
    private bool playerInAttackRange = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Si el jugador no está en nuestro rango, patrullamos.
        if (!playerInAttackRange)
        {
            Patrol();
        }
        else
        {
            // Si el jugador SÍ está en nuestro rango, dejamos de movernos.
            rb.linearVelocity = Vector2.zero;
        }
    }

    void Patrol()
    {
        bool isTouchingWall = Physics2D.OverlapCircle(wallCheckPoint.position, checkRadius, whatIsGround);
        bool isNearLedge = !Physics2D.OverlapCircle(ledgeCheckPoint.position, checkRadius, whatIsGround);

        if (isTouchingWall || isNearLedge)
        {
            Flip();
        }

        float moveDirection = isFacingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(moveSpeed * moveDirection, rb.linearVelocity.y);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    // Esta se llama cuando algo entra en nuestra ZONA DE AGRESIÓN (el CircleCollider2D)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInAttackRange = true;
            animator.SetTrigger("attack"); // Inicia la animación de ataque

            // Hacemos daño aquí por ahora
            if (other.TryGetComponent<Health>(out Health playerHealth))
            {
                playerHealth.TakeDamage(contactDamage,this.transform);
            }
        }
    }

    // Esta se llama cuando algo sale de nuestra ZONA DE AGRESIÓN
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInAttackRange = false;
        }
    }

    
}
