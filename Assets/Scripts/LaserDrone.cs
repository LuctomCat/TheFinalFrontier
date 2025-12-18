using UnityEngine;
using System.Collections;

public class LaserDrone : MonoBehaviour
{
    [Header("Movement Positions")]
    public Vector3 startPosition = new Vector3(5f, 4.1f, 0f);
    public Vector3 endPosition = new Vector3(5f, -4.1f, 0f);

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float activeDuration = 2f;

    [Header("Laser")]
    public GameObject laserObject;

    bool isActive = false;
    Coroutine activeRoutine;

    void Awake()
    {
        transform.position = startPosition;

        if (laserObject != null)
            laserObject.SetActive(false);
    }

    public void Activate()
    {
        if (isActive) return;

        if (activeRoutine != null)
            StopCoroutine(activeRoutine);

        activeRoutine = StartCoroutine(DroneRoutine());
    }

    IEnumerator DroneRoutine()
    {
        isActive = true;

        if (laserObject != null)
            laserObject.SetActive(true);

        // Move down
        while (Vector3.Distance(transform.position, endPosition) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                endPosition,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        yield return new WaitForSeconds(activeDuration);

        // Move back up
        while (Vector3.Distance(transform.position, startPosition) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                startPosition,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        if (laserObject != null)
            laserObject.SetActive(false);

        isActive = false;
        activeRoutine = null;
    }
}
