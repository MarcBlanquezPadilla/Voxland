using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : Equipment {

	private Rock lastRock;

	public override void Use() {

		if (!GameManager.Instance.playerController.ReturnUsingEquip()) {

			GameManager.Instance.playerController.UsingEquip(true);
			GameManager.Instance.playerController.ChangeAnimation(3);

			StartCoroutine(C_UsePickaxe());
		}
	}

	private void Update() {
		if (!ScriptingManager.scriptingMode)
        {
			RaycastHit hit;
			if (Physics.Raycast(GameManager.Instance.player.transform.position + Vector3.up, GameManager.Instance.player.transform.forward, out hit, 1.5f)) {

				if (hit.collider.transform.parent.TryGetComponent<Rock>(out Rock rock)) {

					lastRock = rock;
					rock.Target();
				}
			}
			else {
				if (lastRock != null && lastRock.ReturnTarget())
					lastRock.Untarget();
			}
        }
			
	}

	IEnumerator C_UsePickaxe() {

		yield return new WaitForSeconds(0.5f);

		GameManager.Instance.playerController.UsingEquip(false);

		if (GameManager.Instance.playerController._anim.GetCurrentAnimatorStateInfo(0).IsName("Swing")) {

			RaycastHit hit;
			if (Physics.Raycast(GameManager.Instance.player.transform.position + Vector3.up, GameManager.Instance.player.transform.forward, out hit, 1.5f)) {

				if (hit.collider.transform.parent.TryGetComponent<Rock>(out Rock rock))
					hit.collider.transform.parent.GetComponent<HealthBehaviour>().Hurt(1);
			}
		}
	}
}
