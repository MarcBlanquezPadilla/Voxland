using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {

		if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {

			GameManager.Instance.player.transform.position = new Vector3(0, 1.5f, 0);
			GameManager.Instance.playerController.hitPosition = new Vector3(0, 0, 0);

		}
	}
}
