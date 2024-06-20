using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

	public Item item;

	public void PickupItem() {

		InventoryManager.Instance.AddItem(item);
	}

	private void OnTriggerEnter(Collider other) {

        PickupItem();
		gameObject.GetComponentInParent<DestroyDeactivateBehavoiur>().DestroyGameObject();
    }
}
