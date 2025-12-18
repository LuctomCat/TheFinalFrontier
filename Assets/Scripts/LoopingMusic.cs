using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LoopingMusic : MonoBehaviour
{
    public AudioClip musicClip;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    void Start()
    {
        if (musicClip != null)
        {
            audioSource.clip = musicClip;
            audioSource.Play();
        }
    }

    void Update()
    {
        if (SoundManager.Instance != null)
        {
            audioSource.volume = SoundManager.Instance.GetMusicVolume();
        }
    }
}
