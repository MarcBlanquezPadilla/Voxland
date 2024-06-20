using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Interactable {

	[SerializeField] private Item itemToCollect;

	public override void Interact() {

		InventoryManager.Instance.AddItem(itemToCollect);
		GetComponent<DestroyDeactivateBehavoiur>().DestroyGameObject();
	}
}