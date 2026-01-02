using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        // Carrega valores salvos (opcional, por enquanto pode deixar fixo)
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.8f);
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 0.8f);
    }

    public void OnMusicVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        // Aqui você conectaria com um AudioManager depois
    }

    public void OnSfxVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("SfxVolume", value);
        // Idem para efeitos sonoros
    }

    public void OnBackButton()
    {
        gameObject.SetActive(false);
    }
}