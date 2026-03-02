using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumenSetting : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider generalSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    public void SetGlobalVolume(float volume)
    {
        audioMixer.SetFloat("GlobalVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("GameVolume", volume);
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void SetEfectVolume(float volume)
    {
        audioMixer.SetFloat("EfectVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("EfectVolume", volume);
    }

    void LoadGeneralVolume() 
    {
        generalSlider.value = PlayerPrefs.GetFloat("GameVolume", 1f);
        SetGlobalVolume(generalSlider.value == 0 ? 1f : generalSlider.value);
    }

    void LoadMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        SetMusicVolume(musicSlider.value == 0 ? 1f : musicSlider.value);
    }

    void LoadSfxVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("EfectVolume", 1f);
        SetEfectVolume(sfxSlider.value == 0 ? 1f : sfxSlider.value);
    }
    private void Awake()
    {
        LoadGeneralVolume();
        LoadMusicVolume();
        LoadSfxVolume();
        gameObject.SetActive(false);
    }
}
