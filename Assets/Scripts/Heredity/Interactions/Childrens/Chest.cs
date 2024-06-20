using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemToGive
{
	public Item item;
    public int quantity;
}

public class Chest : Interactable {

	[SerializeField] private List<ItemToGive> itemsToGive;
    [SerializeField] private UnityEvent onOpen;

    public override void Interact() {

        for (int i = 0; i < itemsToGive.Count; i++)
        {
            InventoryManager.Instance.AddItem(itemsToGive[i].item);
            onOpen.Invoke();
            AudioManager.Instance.playAudio("OpenChest");
        }
        GetComponent<Animator>().SetTrigger("Open");
        transform.GetChild(0).GetComponent<InteractableArea>().DeactivateInteractableArea();
        
    }
}
