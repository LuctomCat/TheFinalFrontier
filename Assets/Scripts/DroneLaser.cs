using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
public class DroneLaser : MonoBehaviour
{
    [Header("Damage")]
    public int damage = 1;
    public float damageInterval = 0.25f;

    [Header("Audio")]
    public AudioClip laserLoop;

    float damageTimer;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.loop = true;
        audioSource.playOnAwake = false;

        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void OnEnable()
    {
        damageTimer = 0f;

        if (laserLoop != null)
        {
            audioSource.clip = laserLoop;
            audioSource.Play();
        }
    }

    void OnDisable()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        damageTimer -= Time.deltaTime;
        if (damageTimer > 0f) return;

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            damageTimer = damageInterval;
        }
    }
}
