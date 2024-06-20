using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour {

    Item item;

    public Button removeButton;

    private GameObject player;
    private PlayerController playerController;


    private void Awake() {

        player = GameManager.Instance.player;
        playerController = GameManager.Instance.playerController;
    }

    public void DropItem() {

        playerController.ChangeAnimation(4);
        GameObject Obj = Instantiate(item.itemObj, playerController.droppingPoint.transform.position, Quaternion.identity);
        Obj.GetComponent<Rigidbody>().AddForce(player.transform.forward.x*3, 5, player.transform.forward.z*3, ForceMode.Impulse);
        InventoryManager.Instance.ShowItemName(false, item.stringKey);
        RemoveItem();
    }

    public void RemoveItem() {

        ShowItemName(false);
        InventoryManager.Instance.RemoveItem(item);
    }

    public void SetItem(Item newItem){

        item = newItem;
    }

    public void SetRemoveButton()
    {
        if (item.itemType == Item.ItemType.NotDropeable || item.itemType == Item.ItemType.Map) {

            removeButton.gameObject.SetActive(false);
        }
        else {

            removeButton.gameObject.SetActive(true);
        }
    }

    public void UseItem() {

        switch (item.itemType) {

            case Item.ItemType.Default:
                break;
            case Item.ItemType.Potion:
                GameManager.Instance.player.GetComponent<HealthBehaviour>().AddHealt(item.value);
                break;
            case Item.ItemType.Map:
                InventoryManager.Instance.ShowItemName(false, item.stringKey);
                GameManager.Instance.crafting.SetActive(false);
                GameManager.Instance.inventory.SetActive(false);
                GameManager.Instance.cameraMap.transform.position = new Vector3(GameManager.Instance.player.transform.position.x, GameManager.Instance.cameraMap.transform.position.y, GameManager.Instance.player.transform.position.z);
                GameManager.Instance.playerIconeMap.transform.position = new Vector3(GameManager.Instance.player.transform.position.x, GameManager.Instance.playerIconeMap.transform.position.y, GameManager.Instance.player.transform.position.z);
                GameManager.Instance.cameraMap.SetActive(true);
                GameManager.Instance.map.SetActive(true);
                GuideManager.Instance.DisableGuide();
                break;

        }

        if (item.itemType != Item.ItemType.Resource && item.itemType != Item.ItemType.NotDropeable && item.itemType != Item.ItemType.Map)
            RemoveItem();
    }

    public void UnshowItemName()
    {

    }

    public void ShowItemName(bool b) {

        InventoryManager.Instance.ShowItemName(b, item.stringKey);
    }
}
