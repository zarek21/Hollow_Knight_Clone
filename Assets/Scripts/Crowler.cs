using UnityEngine;

public class Crowler : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField]
    private float speed = 2f;
    public bool movingRight = true;

    [Header("Detección")]
    public Transform groundCheck;
    public Transform wallCheck;
    public float checkDistanceX = 1f;
    public float checkDistanceY = 0.3f;
    public LayerMask ayerMaskWall;

    Rigidbody2D enemyRb;
    SpriteRenderer spriteRenderer;

    //
    void Awake()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void FixedUpdate()
    {
        float moveDir = movingRight ? 1f : -1f;
        enemyRb.linearVelocity = new Vector2(moveDir * speed, enemyRb.linearVelocity.y);

        // Detectar pared y falta de suelo
        bool hitWall = Physics.Raycast(wallCheck.position, movingRight ? Vector2.right : Vector2.left, checkDistanceX, ayerMaskWall);
        bool noGround = !Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistanceY, ayerMaskWall);

        // Cambiar dirección
        if (hitWall || noGround)
        {
            Flip();
        }
    }

    private void Update()
    {
        Debug.DrawRay(wallCheck.position, (movingRight ? Vector2.right : Vector2.left) * checkDistanceX, Color.red);
        Debug.DrawRay(groundCheck.position, Vector2.down * checkDistanceY, Color.blue);

    }

    void Flip()
    {
        Debug.Log("Flip");
        //rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // Detener el movimiento horizontal antes de girar
        movingRight = !movingRight;
        // Invierte solo el sprite (no el transform completo)
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}