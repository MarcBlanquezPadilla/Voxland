using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NPC : Interactable {

	[SerializeField] private Script scriptToExecute;
	[SerializeField] private Color nameTextColor;

	public override void Interact() {

		GameManager.Instance.npcNameText.GetComponent<TextMeshProUGUI>().text = gameObject.name;
		GameManager.Instance.npcNameText.GetComponent<TextMeshProUGUI>().color = nameTextColor;
		ScriptingManager.Instance.StartScript(scriptToExecute);
	}

	public void SwitchScript (Script script) {

		scriptToExecute = script;
    }
}
