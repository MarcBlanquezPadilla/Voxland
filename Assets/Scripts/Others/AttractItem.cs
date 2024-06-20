using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractItem : MonoBehaviour {

	private MovementBehaviour _mb;
	private SphereCollider attractArea;

	private void Start() {

		_mb = gameObject.GetComponent<MovementBehaviour>();
		attractArea = gameObject.GetComponent<SphereCollider>();
		attractArea.enabled = false;
	}

	private void OnTriggerStay(Collider other) {

		if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {

			Vector3 direction = GameManager.Instance.player.GetComponent<PlayerController>().attractPoint.transform.position - gameObject.transform.position;
			direction.Normalize();
			_mb.MoveRb3D(direction);
		}
	}

	private void OnCollisionEnter(Collision collision) {

		attractArea.enabled = true;
	}

	private void OnCollisionExit(Collision collision) {
		attractArea.enabled = false;
	}
}
