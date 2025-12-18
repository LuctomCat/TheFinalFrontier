using UnityEngine;
using UnityEngine.UI;

public class OptionsAudioUI : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        if (SoundManager.Instance == null) return;

        musicSlider.value = SoundManager.Instance.GetMusicVolume();
        sfxSlider.value = SoundManager.Instance.GetSFXVolume();

        musicSlider.onValueChanged.AddListener(OnMusicChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXChanged);
    }

    void OnMusicChanged(float value)
    {
        SoundManager.Instance.SetMusicVolume(value);
    }

    void OnSFXChanged(float value)
    {
        SoundManager.Instance.SetSFXVolume(value);
    }
}
