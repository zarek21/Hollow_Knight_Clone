// using - Librerías -  Funciones prestadas de otros scripts 
using UnityEngine;

/// <summary>
///  Zarek 03 septiembre 2025
/// </summary>


public class MovementPlayer : MonoBehaviour
{
    //Variables 
    public Transform transformPlayer;

    public Rigidbody2D rigidBody2DPlayer;

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
    }//end Start


    // Desde Frame 2 hasta que termine el juego
    //Loop que se llama toooodo el tiempo 
    //Funciona mejor o peor dependiente de la PC, no tiene la
    void Update()
    {
        //Movimiento Izq
        if (Input.GetKeyDown(KeyCode.A))
        {
            print("Vamos a la izq");

            //Ignora las fisicas 
            //transformPlayer.position += new Vector3(-1f,0f,0f);
            rigidBody2DPlayer.AddForce(Vector2.left * 5.0f);

        }

        //Movimiento Der
        if (Input.GetKeyDown(KeyCode.D))
        {
            print("Vamos a la Der");
            //transformPlayer.position +=  Vector3.right;
            rigidBody2DPlayer.AddForce(Vector2.left * 5.0f);

        }
        /*
        print("Update inicia aqui");
        if(Input.GetKeyDown (KeyCode.A))
        {
            //transformPlayer.position += new Vector3(1,0,0);
            //transformPlayer.position +=  Vector3.right * 1f;
            rigidbodyPlayer.AddForce(Vector2.right);
        }
        */

    }//end Update


    //Tasa fija de Frames
    private void FixedUpdate()
    {

    }

}  // end - class - MovementPlayer
