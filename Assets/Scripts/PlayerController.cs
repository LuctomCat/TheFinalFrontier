using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float minY = -4f;
    public float maxY = 4f;
    public float clampX = 7.5f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.3f;

    [Header("Reload")]
    public int maxShots = 10;
    public float reloadTime = 2f;

    float fireTimer;
    int shotsRemaining;
    bool reloading;

    public bool IsReloading => reloading;

    void Start()
    {
        shotsRemaining = maxShots;
        transform.position = new Vector3(clampX, transform.position.y, 0f);
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        float v = Input.GetKey(KeyCode.W) ? 1f : Input.GetKey(KeyCode.S) ? -1f : 0f;
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y + v * moveSpeed * Time.deltaTime, minY, maxY);
        pos.x = clampX;
        transform.position = pos;
    }

    void HandleShooting()
    {
        fireTimer -= Time.deltaTime;
        if (reloading || fireTimer > 0f) return;

        if (shotsRemaining <= 0)
        {
            reloading = true;
            Invoke(nameof(FinishReload), reloadTime);
            return;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            fireTimer = fireRate;
            shotsRemaining--;
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }

    void FinishReload()
    {
        shotsRemaining = maxShots;
        reloading = false;
    }
}
