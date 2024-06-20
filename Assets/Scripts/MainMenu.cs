using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void StartGame() {

        Invoke("StartGame2", 6);
    }

    private void StartGame2() {

        SceneManager.LoadSceneAsync("LoadScene");
    }

    public void Exit() {

        Application.Quit();
    }
}
