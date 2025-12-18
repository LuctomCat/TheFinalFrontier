using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class DroneButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LaserDrone drone;

    [Header("Cooldown Settings")]
    [SerializeField] float cooldownTime = 10f;

    [Header("Button Colors")]
    [SerializeField] Color readyColor = Color.green;
    [SerializeField] Color cooldownColor = Color.red;

    bool isReady = true;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;

        sr.color = readyColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isReady) return;

        if (other.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Drone Button Activated");
            ActivateButton();
        }
    }

    void ActivateButton()
    {
        isReady = false;
        sr.color = cooldownColor;

        if (drone != null)
            drone.Activate();
        else
            Debug.LogWarning("DroneButton has no drone assigned!");

        StartCoroutine(CooldownRoutine());
    }

    IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        isReady = true;
        sr.color = readyColor;
    }
}
