using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour {

	[SerializeField] private Material emmisiveMaterial;
	[SerializeField] private Material defaultMaterial;

	public void OnHover() {

		StopAllCoroutines();
		StartCoroutine(C_EnableEmisionSinceATime());
    }

	public abstract void Interact();

	IEnumerator C_EnableEmisionSinceATime() {

		MeshRenderer[] mesh = GetComponentsInChildren<MeshRenderer>();

		for (int i = 0; i < mesh.Length; i++) {

			mesh[i].material = emmisiveMaterial;
		}
		
		yield return new WaitForSeconds(0.01f);

		for (int i = 0; i < mesh.Length; i++) {

			mesh[i].material = defaultMaterial;
		}
	}
}
