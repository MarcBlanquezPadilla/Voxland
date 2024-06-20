using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private float degrees = 0;

	private void Update() {

		if (!GameManager.Instance.playerController.blockControls) {

			Vector3 playerPos = GameManager.Instance.player.transform.position;
			transform.position = new Vector3(playerPos.x, transform.position.y, playerPos.z);

			Quaternion target = Quaternion.Euler(0, degrees, 0);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * 500);
		}

		RaycastHit[] hit;

		hit = Physics.RaycastAll(transform.GetChild(0).transform.position + transform.GetChild(0).transform.forward * -10, GameManager.Instance.player.transform.position - (transform.GetChild(0).transform.position + transform.GetChild(0).transform.forward * -10), Vector3.Distance(transform.GetChild(0).position + transform.GetChild(0).transform.forward * -10, GameManager.Instance.player.transform.position));

		for (int i = 0; i < hit.Length; i++) {

			if (hit[i].collider.TryGetComponent<FadeObjects>(out FadeObjects fadeObjects)) 
				fadeObjects.Fade();

		}		
    }
}
