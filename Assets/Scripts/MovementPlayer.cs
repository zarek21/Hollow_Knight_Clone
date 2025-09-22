// using - Librerías -  Funciones prestadas de otros scripts

using Unity.VisualScripting;
using UnityEngine;

/// <summary>
///  Zarek 03 septiembre 2025
/// </summary>
public class MovementPlayer : MonoBehaviour
{
    // Variables
    public Transform transformPlayer;
    public Rigidbody2D rigidBody2DPlayer;
    public SpriteRenderer spriteRenderer;
    public Vector2 movement; // set movement input variable (x,y)

    // Private Variables
    private bool isGrounded = false;

    [Header("PLAYER STATS")]
    [SerializeField]
    private float moveSpeed = 5.0f;

    [SerializeField]
    private float jumpForce = 15.0f;

    [SerializeField]
    private float downardForce = 25.0f;

    void Start()
    {
        print(GameReferences.Texts.START);
        //Jala el rb del objeto
        rigidBody2DPlayer = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    } //end Start

    void Update()
    {
        // Movimiento Izq
        if (Input.GetKeyDown(KeyCode.A))
        {
            print(GameReferences.Texts.VAMOS_A_LA_IZQUIERDA);

            //Ignora las fisicas

            //transformPlayer.position += new Vector3(-1f,0f,0f);


            // Cambiamos su dirección en el eje x

            spriteRenderer.flipX = false;
        }

        // Movimiento Der
        if (Input.GetKeyDown(KeyCode.D))
        {
            print(GameReferences.Texts.VAMOS_A_LA_DERECHA);

            //transformPlayer.position +=  Vector3.right;

            // Cambiamos su dirección en el eje x
            spriteRenderer.flipX = true;
        }

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            print(GameReferences.Texts.YA_NO_ESTAS_EN_EL_SUELO_Y_ESTAS_SALTANDO);

            rigidBody2DPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        movement = new Vector2(Input.GetAxis("Horizontal"), 0.0f);
    } //end Update

    private void FixedUpdate()
    {
        /* Esto simula la fuerza de empuje hacia abajo
        para logar un efecto más disfrutable al saltar */
        rigidBody2DPlayer.AddForce(Vector2.down * downardForce, ForceMode2D.Force);
        moveCharacter(movement);
        
        
    } // end of FixedUpdate


    void moveCharacter(Vector2 direction) 
    {
        rigidBody2DPlayer.linearVelocity = new Vector2(direction.x * moveSpeed, rigidBody2DPlayer.linearVelocity.y);
    }

    // Colisión para entradas
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Colisión con "Ground"
        if (collision.gameObject.CompareTag(GameReferences.Tags.GROUND))
        {
            print(GameReferences.Texts.ESTAS_EN_EL_SUELO);

            isGrounded = true;
        }

    }

    // Colisión para salidas

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Colisión con "Ground"
        if (collision.gameObject.CompareTag(GameReferences.Tags.GROUND))
        {
            print(GameReferences.Texts.ESTAS_FUERA_DEL_SUELO);

            isGrounded = false;
        }
    } // end of onCollisionExit
} // end - MovementPlayer
