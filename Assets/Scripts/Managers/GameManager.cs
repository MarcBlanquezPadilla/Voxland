using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	#region Instance

	private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    #endregion

    [Header("Objects")]
    public GameObject player;
    public GameObject inventory;
    public GameObject crafting;
    public GameObject cam;
    public GameObject npcNameText;
    public GameObject map;
    public GameObject cameraMap;
    public GameObject playerIconeMap;
    public GameObject pauseMenu;

    [Header("References")]
    public PlayerController playerController;
    public CameraController cameraController;
    [SerializeField] private Script scriptToExecute;

    private void Awake() {

        playerController = player.GetComponent<PlayerController>();
        cameraController = cam.GetComponent<CameraController>();
	}

    private void Start() {
        
        ScriptingManager.Instance.StartScript(scriptToExecute);
    }

	public void Exit() {

        Application.Quit();
    }
}
