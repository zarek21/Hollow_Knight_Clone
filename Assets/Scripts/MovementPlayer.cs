// using - Librerías -  Funciones prestadas de otros scripts 
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
///  Zarek 03 septiembre 2025
/// </summary>


public class MovementPlayer : MonoBehaviour
{
    // Variables 
    public Transform transformPlayer;
    public Rigidbody2D rigidBody2DPlayer;
    public SpriteRenderer spriteRenderer;

    // Private Variables
    private bool isGrounded = false ;

    [Header("PLAYER STATS")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 10.0f;

    /*
public Transform transformPlayer;
public Rigidbody2D rigidbodyPlayer;
    */

    // donde empieza el Frame 1. Frame 2 dejo de llamarse 
    void Start()
    {
        print("Start inicia aqui");
        //Jala el rb del objeto
        rigidBody2DPlayer = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    } //end Start

    void Update()
    {
        // Movimiento Izq
        if (Input.GetKeyDown(KeyCode.A))
        {
            print("Vamos a la izq");

            //Ignora las fisicas 
            //transformPlayer.position += new Vector3(-1f,0f,0f);
            rigidBody2DPlayer.AddForce(Vector2.left * moveSpeed);
            
            // Cambiamos su dirección en el eje x
            spriteRenderer.flipX = false;

        }

        // Movimiento Der
        if (Input.GetKeyDown(KeyCode.D))
        {
            print("Vamos a la Der");
            //transformPlayer.position +=  Vector3.right;
            rigidBody2DPlayer.AddForce(Vector2.right * moveSpeed);
            
            // Cambiamos su dirección en el eje x
            spriteRenderer.flipX = true;
        }

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            print("Ya no estas tocando el suelo y estas saltando");
            rigidBody2DPlayer.AddForce(Vector3.up * jumpForce,ForceMode2D.Impulse);
        }

    }//end Update


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            print("Estas fuera de el suelo");
            isGrounded = false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            print("Estas en el suelo");
            isGrounded = true;
        }

    }

}  // end - class - MovementPlayer
