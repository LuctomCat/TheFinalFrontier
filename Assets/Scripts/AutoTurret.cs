using UnityEngine;

public class AutoTurret : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireInterval = 1.2f;

    [Header("Audio")]
    public AudioClip fireSound;

    float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = fireInterval;

            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            PlaySFX(fireSound);
        }
    }

    // Audio Controller
    void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        float volume = SoundManager.Instance != null
            ? SoundManager.Instance.GetSFXVolume()
            : 1f;

        AudioSource.PlayClipAtPoint(clip, transform.position, volume);
    }
}
