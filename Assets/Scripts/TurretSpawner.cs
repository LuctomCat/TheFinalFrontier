using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    public Transform[] turretSlots;
    public GameObject turretPrefab;
    public float spawnInterval = 20f;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            TrySpawn();
        }
    }

    void TrySpawn()
    {
        foreach (Transform slot in turretSlots)
        {
            if (slot.childCount == 0)
            {
                Instantiate(turretPrefab, slot.position, Quaternion.identity, slot);
                return;
            }
        }
    }
}
