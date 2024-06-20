using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour {

    private void Start() {

        Invoke("StartGame", 1);
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("language"))
        {
            SetIdiom(PlayerPrefs.GetInt("language"));
        }
        else
        {
            SetIdiom(2);
        }
    }

    public void SetIdiom(int lang)
    {
        TextManager.Instance.ChangeIdiom(lang);
    }

    private void StartGame() {

        SceneManager.LoadSceneAsync("GameScene");
    }
}
