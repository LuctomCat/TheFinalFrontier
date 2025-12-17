using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType { Standard, Fast, Fat }
    public EnemyType enemyType = EnemyType.Standard;

    [Header("Health")]
    public int baseHealth = 3;
    int currentHealth;

    public float moveSpeed = 1.5f;
    public int damageToBarricade = 2;
    public AudioClip deathSound;

    [HideInInspector] public float targetX = 8f;

    void OnEnable()
    {
        currentHealth = baseHealth;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += Mathf.Sign(targetX - pos.x) * moveSpeed * Time.deltaTime;
        transform.position = pos;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        if (deathSound != null)
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
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
