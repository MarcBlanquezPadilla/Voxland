using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    #region Instance

    private static Settings _instance;
    public static Settings Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Settings>();
            }
            return _instance;
        }
    }

    #endregion

    [Header("REFERENCED")]
    [SerializeField] private Toggle toggleFullScreen;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider fxSlider;

    [Header("INFORMATION")]
    [SerializeField] private bool fullScreen;
    [SerializeField] private float master;
    [SerializeField] private float music;
    [SerializeField] private float fx;
    [SerializeField] private int language;

    private void Awake()
    {
        LoadPrefs();
    }

    private void Start()
    {
        LoadPrefs();
    }

    public void SavePrefs()
    {
        int k = (fullScreen == true) ? 1 : 0;
        PlayerPrefs.SetInt("fullScreen", k);
        PlayerPrefs.SetFloat("master", master);
        PlayerPrefs.SetFloat("music", music);
        PlayerPrefs.SetFloat("fx", fx);
        PlayerPrefs.SetInt("language", language);

        PlayerPrefs.Save();
    }

    public void LoadPrefs()
    {
        if (PlayerPrefs.HasKey("fullScreen"))
        {
            bool k = (PlayerPrefs.GetInt("fullScreen") == 1 ? true : false);
            SetFullScreen(k);
        }
        else
        {
            SetFullScreen(true);
        }
        if (PlayerPrefs.HasKey("master"))
        {
            SetMasterVolume(PlayerPrefs.GetFloat("master"));
        }
        else
        {
            SetMasterVolume(1);
        }
        if (PlayerPrefs.HasKey("music"))
        {
            SetMusicVolume(PlayerPrefs.GetFloat("music"));
        }
        else
        {
            SetMusicVolume(1);
        }
        if (PlayerPrefs.HasKey("fx"))
        {
            SetFxVolume(PlayerPrefs.GetFloat("fx"));
        }
        else
        {
            SetFxVolume(1);
        }
        if (PlayerPrefs.HasKey("language"))
        {
            SetIdiom(PlayerPrefs.GetInt("language"));
        }
        else
        {
            SetIdiom(2);
        }
    }

    public void ChangeFullScreen(bool ToggleBool)
    {
        fullScreen = ToggleBool;
        Screen.fullScreen = fullScreen;
    }

    public void SliderMasterVolume(float sliderValue)
    {
        master = sliderValue;
        mixer.SetFloat("VolumeMaster", GetMixerVolume(master));
    }

    public void SliderMusicVolume(float sliderValue)
    {
        music = sliderValue;
        mixer.SetFloat("VolumeMusic", GetMixerVolume(music));
    }

    public void SliderFxVolume(float sliderValue)
    {
        fx = sliderValue;
        mixer.SetFloat("VolumeFx", GetMixerVolume(fx));
    }

    public void SetFullScreen(bool full)
    {
        ChangeFullScreen(full);
        toggleFullScreen.isOn = fullScreen;
    }

    public void SetMasterVolume(float masterVolume)
    {

        master = masterVolume;
        masterSlider.value = master;
        mixer.SetFloat("VolumeMaster", GetMixerVolume(master));
    }

    public void SetMusicVolume(float musicVolume)
    {
        music = musicVolume;
        musicSlider.value = music;
        mixer.SetFloat("VolumeMusic", GetMixerVolume(music));
    }

    public void SetFxVolume(float fxVolume)
    {
        fx = fxVolume;
        fxSlider.value = fx;
        mixer.SetFloat("VolumeFx", GetMixerVolume(fx));
    }

    public void SetIdiom(int lang)
    {
        language = lang;
        TextManager.Instance.ChangeIdiom(lang);
    }

    public float GetMixerVolume(float num)
    {
        if (num != 0)
        {
            return Mathf.Log10(num) * 20;
        }
        else
        {
            return -80;
        }
    }
}
