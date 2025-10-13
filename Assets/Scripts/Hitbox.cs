using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int attackDamage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Health>(out Health enemyHealth))
        {
            enemyHealth.TakeDamage(attackDamage);
            Debug.Log("Golpeado: " + other.name);
        }
    }
}
