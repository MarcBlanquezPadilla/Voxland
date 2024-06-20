using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeededItemController : MonoBehaviour
{
    private Item item;

    public void SetItem(Item i)
    {
        item = i;
    }
    public void ShowItemName(bool b)
    {
        InventoryManager.Instance.ShowItemName(b, item.stringKey);
    }
}
