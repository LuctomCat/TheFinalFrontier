using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Units per second")]
    public float moveSpeed = 5f;
    [Tooltip("Min and max Y positions player can travel")]
    public float minY = -4f;
    public float maxY = 4f;
    [Tooltip("X position (world) where player is locked. Keep on right half.")]
    public float clampX = 7.5f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    [Tooltip("Where bullets originate — set an empty child transform over the barricade.")]
    public Transform firePoint;
    public float fireRate = 0.3f; // fire rate -----

    float fireTimer = 0f;

    void Start()
    {
        // sets starting position.
        Vector3 p = transform.position;
        p.x = clampX;
        transform.position = p;
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        float v = 0f;
        if (Input.GetKey(KeyCode.W)) v = 1f;
        if (Input.GetKey(KeyCode.S)) v = -1f;

        Vector3 pos = transform.position;
        pos.y += v * moveSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.x = clampX; // keep locked to the correct half of the screen
        transform.position = pos;
    }

    void HandleShooting()
    {
        fireTimer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && fireTimer <= 0f)
        {
            fireTimer = fireRate;
            if (bulletPrefab != null && firePoint != null)
            {
                Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            }
        }
    }
}
