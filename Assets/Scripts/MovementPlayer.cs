// using - Librerías -  Funciones prestadas de otros scripts 
using UnityEngine;

/// <summary>
///  Zarek 03 septiembre 2025
/// </summary>


public class MovementPlayer : MonoBehaviour
{
    // Variables
    [Header("Propiedades")]
    public Transform transformPlayer;

    [Header("Configuraciones")]
    public float playerSpeed = 0.0f;

    private void Start()
    {
    }

    private void Update()
    {
        // Movimiento Izquierda
        if (Input.GetKeyDown(KeyCode.A)) 
        {
            print("Vamos a la izquierda");
        }

        // Movimiento Derecha
        if (Input.GetKeyDown(KeyCode.D))
        {
            print("Vamos a la derecha");
        }

    }

}  // end - class - MovementPlayer
