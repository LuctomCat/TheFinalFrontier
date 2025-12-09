using UnityEngine;
public class Enemy : MonoBehaviour
{
    public enum EnemyType { Standard, Fast, Fat }
    public EnemyType enemyType = EnemyType.Standard;

    [HideInInspector] public int maxHealth = 3;
    int currentHealth;
    public float moveSpeed = 1.5f;
    public int damageToBarricade = 2;

    // target position of the barricade
    [HideInInspector] public float targetX = 8f;

    void OnEnable()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Move towards the right side of the screen
        Vector3 pos = transform.position;
        float dir = Mathf.Sign(targetX - pos.x);
        pos.x += dir * moveSpeed * Time.deltaTime;
        transform.position = pos;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Barricade barricade = other.GetComponent<Barricade>();
        if (barricade != null)
        {
            barricade.TakeDamage(damageToBarricade);
            Destroy(gameObject);
        }
    }
}
