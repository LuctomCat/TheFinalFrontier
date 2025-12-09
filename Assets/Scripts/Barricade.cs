using UnityEngine;
using UnityEngine.Events;

public class Barricade : MonoBehaviour
{
    public int maxHealth = 20;
    public int currentHealth;

    public UnityEvent onDestroyed;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            onDestroyed?.Invoke();
        }
    }
}

