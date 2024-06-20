using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unarmed : Equipment {

    private LayerMask interactable;
    private void Start() {

        interactable = GameManager.Instance.player.GetComponent<PlayerController>().interactable;
    }

    public override void Use() {

        StopAllCoroutines();

        Vector2 mousePosition;

        RaycastHit hit;
        mousePosition = Input.mousePosition;
        Ray r = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(r, out hit, 50, interactable)) {

            if (hit.collider.gameObject.GetComponent<NPC>() != null) hit.collider.gameObject.GetComponent<NPC>().Interact();
         }
    }
}
