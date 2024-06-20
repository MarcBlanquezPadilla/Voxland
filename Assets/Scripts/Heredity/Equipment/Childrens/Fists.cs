using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fists : Equipment {

    private LayerMask interactable;

    private void Start() {

        interactable = GameManager.Instance.player.GetComponent<PlayerController>().interactable;
    }

    private void Update() {

        if (!ScriptingManager.scriptingMode && !GameManager.Instance.playerController.blockControls) {

            RaycastHit[] hit;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics.RaycastAll(r, Mathf.Infinity, interactable);

            for (int i = 0; i < hit.Length; i++)
            {

                if (hit[i].collider.gameObject.TryGetComponent<Interactable>(out Interactable _in))
                    _in.OnHover();
            }
        }
    }


    public override void Use() {

        StopAllCoroutines();

        RaycastHit[] hit;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics.RaycastAll(r, Mathf.Infinity, interactable);

        for (int i = 0; i < hit.Length; i++)
        {

            if (hit[i].collider.gameObject.GetComponent<Interactable>()) hit[i].collider.gameObject.GetComponent<Interactable>().Interact();
            if (hit[i].collider.gameObject.GetComponent<Enemy>()) hit[i].collider.gameObject.GetComponent<Enemy>().ChangeBaseColorSinceATime(false);
            //if (hit.collider.gameObject.GetComponent<Chest>() != null) hit.collider.gameObject.GetComponent<Enemy>().ChangeBaseColorSinceATime(false);
        }
    }
}
