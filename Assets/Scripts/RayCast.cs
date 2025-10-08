using UnityEngine;

public class RayCast : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D rb;
    private bool isFacingRight = true; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float moveDirection = isFacingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(moveSpeed * moveDirection, rb.linearVelocity.y);

        Vector2 rayDirection = isFacingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, wallCheckDistance, wallLayer);
        Debug.DrawRay(transform.position, rayDirection * wallCheckDistance, Color.red);

        if (hit.collider != null)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        // Invertimos la escala del objeto para que el sprite mire en la nueva dirección
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
