using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject hitbox; 
    [SerializeField] private float attackDuration = 0.2f;

  
    public void OnAttack(InputAction.CallbackContext context)
    {
      
        if (context.performed)
        {
        
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