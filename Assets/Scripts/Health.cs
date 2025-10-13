using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float knockbackForce = 5.0f;
    private int currentHealth;

    private MovementPlayer movementPlayer; // REFERENCIA AL SCRIPT DE MOVIMIENTO

    [Header("EFECTOS DE MUERTE")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject deathEffectPrefab;
    private void Awake()
    {
        currentHealth = maxHealth;
        movementPlayer = GetComponent<MovementPlayer>(); // OBTENEMOS LA REFERENCIA
    }


    public void TakeDamage(int damageAmount, Transform damageSource)
    {
        currentHealth -= damageAmount;
        Debug.Log(gameObject.name + " recibió " + damageAmount + " de daño. Vida restante: " + currentHealth);

        if (movementPlayer != null)
        {
            Vector2 knockbackDirection = (transform.position - damageSource.position).normalized;
            movementPlayer.ApplyKnockback(knockbackDirection,knockbackForce);
        }

        if (currentHealth <= 0) 
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " ha muerto.");

        if (coinPrefab != null) 
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }

        if (deathEffectPrefab != null) 
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
