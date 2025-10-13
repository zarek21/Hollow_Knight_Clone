using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("Efectos de Muerte")]
    [SerializeField] private GameObject coinPrefab;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // Si presionamos la tecla 'K'
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(1); // Le hacemos 1 de daño al objeto que tenga este script
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log(gameObject.name + " recibió " + damageAmount + " de daño. Vida restante: " + currentHealth);

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

        Destroy(gameObject);
    }
}
