using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BackPack : Interactable {

    [SerializeField] UnityEvent onOpen;
    [SerializeField] Script tutorial;

    bool firstTime = true;

    public override void Interact() {

        onOpen.Invoke();

        if (firstTime)
        {
            ScriptingManager.Instance.StartScript(tutorial);
            firstTime = false;
        }

        var gameManager = GameManager.Instance;

        gameManager.inventory.SetActive(true);
        gameManager.crafting.SetActive(true);
        gameManager.playerController.ChangeAnimation(5);
        gameManager.playerController.DisableControlls();
    }
}
