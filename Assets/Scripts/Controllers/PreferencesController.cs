using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PreferencesController : MonoBehaviour
{
    private static PreferencesController _instance;
    public static PreferencesController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PreferencesController>();
            }
            return _instance;
        }
    }

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
        //int k = (ScreenController.Instance.GetFullScreen() == true) ? 1 : 0;
        //PlayerPrefs.SetInt("fullScreen", k);
        //int j = (GodModeController.Instance.GetGodMode() == true) ? 1 : 0;
        //PlayerPrefs.SetInt("godMode", j);
        //PlayerPrefs.SetFloat("master", MixerController.Instance.GetMasterVolume());
        //PlayerPrefs.SetFloat("music", MixerController.Instance.GetMusicVolume());
        //PlayerPrefs.SetFloat("fx", MixerController.Instance.GetFxVolume());

        PlayerPrefs.Save();
    }

    public void LoadPrefs()
    {
        //    if (PlayerPrefs.HasKey("fullScreen"))
        //    {
        //        bool k = (PlayerPrefs.GetInt("fullScreen") == 1 ? true : false);
        //        ScreenController.Instance.SetFullScreen(k);
        //    }
        //    else
        //    {
        //        ScreenController.Instance.SetFullScreen(true);
        //    }
        //    if (PlayerPrefs.HasKey("godMode"))
        //    {
        //        bool j = (PlayerPrefs.GetInt("godMode") == 1 ? true : false);
        //        GodModeController.Instance.SetGodMode(j);
        //    }
        //    else
        //    {
        //        GodModeController.Instance.SetGodMode(false);
        //    }
        //    if (PlayerPrefs.HasKey("master"))
        //    {
        //        MixerController.Instance.SetMasterVolume(PlayerPrefs.GetFloat("master"));
        //    }
        //    else
        //    {
        //        MixerController.Instance.SetMasterVolume(1);
        //    }
        //    if (PlayerPrefs.HasKey("music"))
        //    {
        //        MixerController.Instance.SetMusicVolume(PlayerPrefs.GetFloat("music"));
        //    }
        //    else
        //    {
        //        MixerController.Instance.SetMusicVolume(1);
        //    }
        //    if (PlayerPrefs.HasKey("fx"))
        //    {
        //        MixerController.Instance.SetFxVolume(PlayerPrefs.GetFloat("fx"));
        //    }
        //    else
        //    {
        //        MixerController.Instance.SetFxVolume(1);
        //    }
        //
    }
}
