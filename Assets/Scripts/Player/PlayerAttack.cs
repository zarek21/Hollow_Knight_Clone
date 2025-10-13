using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject hitbox; 
    [SerializeField] private float attackDuration = 0.2f;

    // VARIABLES COMPONENTES
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("¡FUNCIÓN ONATTACK LLAMADA!"); // <-- AÑADE ESTA LÍNEA

        if (context.performed)
        {
            animator.SetTrigger("attack");
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        // 1. Activa el hitbox
        Debug.Log("Ataque iniciado!");
        hitbox.SetActive(true);

        // 2. Espera una fracción de segundo
        yield return new WaitForSeconds(attackDuration);

        // 3. Desactiva el hitbox
        Debug.Log("Ataque terminado.");
        hitbox.SetActive(false);
    }
}