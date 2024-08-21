using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadMusicVolume();
        }
        else
        {
            SetMusicVolume();
        }

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadSfxVolume();
        }
        else
        {
            SetSfxVolume();
        }
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("music", Mathf.Log10(musicSlider.value) * 20);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }

    public void SetSfxVolume()
    {
        audioMixer.SetFloat("sfx",Mathf.Log10(sfxSlider.value) * 20 );
        PlayerPrefs.SetFloat("sfxVolume", musicSlider.value);
    }

    private void LoadMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        
        SetMusicVolume();
    }
    
    private void LoadSfxVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        
        SetSfxVolume();
    }
}
