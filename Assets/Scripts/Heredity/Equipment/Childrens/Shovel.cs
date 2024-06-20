using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : Equipment {

	private Buried lastBuried;

	public override void Use() {

		if (!GameManager.Instance.playerController.ReturnUsingEquip()) {

			GameManager.Instance.playerController.UsingEquip(true);
			GameManager.Instance.playerController.ChangeAnimation(7);

			StartCoroutine(C_UseShovel());
		}
	}

	private void Update() {

		if (!ScriptingManager.scriptingMode) {

			RaycastHit hit;
			if (Physics.Raycast(GameManager.Instance.player.transform.position + Vector3.up, -GameManager.Instance.player.transform.up, out hit, 1.5f)) {

				if (hit.collider.transform.parent.TryGetComponent<Buried>(out Buried buriedController))
				{

					lastBuried = buriedController;
					buriedController.Target();
				}
			}
			else {

				if (lastBuried != null && lastBuried.ReturnTarget())
					lastBuried.Untarget();
			}
		}
	}

	IEnumerator C_UseShovel()
	{

		yield return new WaitForSeconds(0.8f);

		GameManager.Instance.playerController.UsingEquip(false);

		if (GameManager.Instance.playerController._anim.GetCurrentAnimatorStateInfo(0).IsName("Scratch"))
		{

			RaycastHit hit;
			if (Physics.Raycast(GameManager.Instance.player.transform.position + Vector3.up, -GameManager.Instance.player.transform.up, out hit, 1.5f))
			{

				if (hit.collider.transform.parent.TryGetComponent<Buried>(out Buried buried))
					hit.collider.transform.parent.GetComponent<HealthBehaviour>().Hurt(1);
			}
		}
	}
}
