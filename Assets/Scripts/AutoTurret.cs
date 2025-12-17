using UnityEngine;

public class AutoTurret : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireInterval = 1.2f;

    float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = fireInterval;
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }
}
