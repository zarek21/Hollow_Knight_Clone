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
    [SerializeField] private float attackCooldown = 2.0f;

    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private Animator animator;

    // VARIABLES PARA SISTEMA DE ESTADOS
    private bool playerInAttackRange = false;

    // VARIABLES LÓGICA GENERAL
    private float attackCooldownTimer = 0f; 
    private Health playerHealth;     
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Si el temporizador está contando, lo reducimos.
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.fixedDeltaTime;
        }

        if (!playerInAttackRange)
        {
            Patrol(); // Si el jugador no está cerca, patrullamos.
        }
        else
        {
            // Si el jugador está cerca, paramos de patrullar.
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            // Y si el temporizador está listo (en cero), atacamos.
            if (attackCooldownTimer <= 0)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        Debug.Log("¡Crawler ataca!");

        // Reiniciamos el temporizador al valor del cooldown
        attackCooldownTimer = attackCooldown;

        // Activamos la animación
        animator.SetTrigger("attack");

        // Hacemos daño al jugador (si todavía tenemos la referencia)
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(contactDamage, this.transform);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInAttackRange = true;
            // Guardamos una referencia a la vida del jugador
            playerHealth = other.GetComponent<Health>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInAttackRange = false;
            playerHealth = null; // Olvidamos al jugador cuando se va
        }
    }


}
