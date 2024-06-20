using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableArea : MonoBehaviour {

	private LayerMask defaultLayer;
	public LayerMask interactableArea;

	private void Awake() {
		
		defaultLayer = gameObject.transform.parent.gameObject.layer;
	}

	private void OnTriggerEnter(Collider other) {

		gameObject.transform.parent.gameObject.layer = LayerMask.NameToLayer("Interactable");
	}

	private void OnTriggerExit(Collider other) {

		gameObject.transform.parent.gameObject.layer = defaultLayer;
	}

	public void DeactivateInteractableArea()
    {
		gameObject.transform.parent.gameObject.layer = defaultLayer;
		gameObject.SetActive(false);

	}
}
