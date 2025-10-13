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

    private Rigidbody2D rb;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Usamos OverlapCircle en lugar de Raycast. Es como un raycast "gordito", más robusto.
        bool isTouchingWall = Physics2D.OverlapCircle(wallCheckPoint.position, checkRadius, whatIsGround);
        bool isNearLedge = !Physics2D.OverlapCircle(ledgeCheckPoint.position, checkRadius, whatIsGround); // ¡Nota la negación '!' aquí!

        // Si tocamos una pared O estamos cerca de un abismo, nos damos la vuelta.
        if (isTouchingWall || isNearLedge)
        {
            Flip();
        }

        // Aplicamos el movimiento
        float moveDirection = isFacingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(moveSpeed * moveDirection, rb.linearVelocity.y);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    // Para poder ver los puntos de detección en el Editor
    private void OnDrawGizmos()
    {
        if (wallCheckPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(wallCheckPoint.position, checkRadius);
        }
        if (ledgeCheckPoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(ledgeCheckPoint.position, checkRadius);
        }
    }
}
