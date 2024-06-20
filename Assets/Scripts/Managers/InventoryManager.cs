using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Items {

    public string key;
    public Item item;
    public int amount;
}

public class InventoryManager : MonoBehaviour {

    #region Instance

    private static InventoryManager _instance;
    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InventoryManager>();
            }
            return _instance;
        }
    }

    #endregion

    [Header("Properties")]
    [SerializeField] private int maxSlots;
    
    [Header("Referenced")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform itemContent;
    [SerializeField] private GameObject itemNameText;
    [SerializeField] private GameObject inventory;

    private int order;
    private Dictionary<string, Items> _items = new Dictionary<string, Items>();

    private void Awake() {

        order = 0;
        inventory.SetActive(false);
    }

    public void AddItem(Item item) {

        if (!_items.ContainsKey(item.name)) {

            Items j = new Items();
            j.key = item.name;
            j.item = item;
            j.amount = 0;
            _items.Add(j.key, j);
        }

        if (item.itemName == "Map" || item.itemName == "Coin")
        {
            NotificationsManager.Instance.ShowNotification(item.icon, "NOTIFICATION03");
        }

        if (itemContent.childCount < maxSlots)
            _items[item.name].amount++;
        else if (itemContent.childCount == maxSlots && (_items[item.name].amount % _items[item.name].item.maxStackeables) != 0)
            _items[item.name].amount++;

        OrderDictionary(order);
        CraftingManager.Instance.UpdateRecipes();
    }

    public void AddAmountOfItem(Item item, int amount) {

        if (!_items.ContainsKey(item.name)) {

            Items j = new Items();
            j.key = item.name;
            j.item = item;
            j.amount = 0;
            _items.Add(j.key, j);
        }


        for (int i = 0; i < amount; i++) {

            if (itemContent.childCount < maxSlots)
                _items[item.name].amount++;
            else if (itemContent.childCount == maxSlots && (_items[item.name].amount % _items[item.name].item.maxStackeables) != 0)
                _items[item.name].amount++;
        }

        OrderDictionary(order);
        CraftingManager.Instance.UpdateRecipes();
    }

    public void RemoveItem(Item item) {

        _items[item.name].amount--;
        if (_items[item.name].amount < 0)
            _items[item.name].amount = 0;

        OrderDictionary(order);
        CraftingManager.Instance.UpdateRecipes();
    }

    public void RemoveAmountOfItem(Item item, int amount) {

        _items[item.name].amount -= amount;
        if (_items[item.name].amount < 0)
            _items[item.name].amount = 0;

        OrderDictionary(order);
        CraftingManager.Instance.UpdateRecipes();
    }

    public void ListItems() {

        for (int i = 0; i < itemContent.childCount; i++) {

            Destroy(itemContent.GetChild(i).gameObject);
        }

        foreach (KeyValuePair<string,Items> item in _items) {

            int stacks = item.Value.amount / item.Value.item.maxStackeables;
            if (item.Value.amount > 0) {

                GameObject obj = null;
                for (int i = 0; i < stacks; i++) {

                    obj = Instantiate(prefab, itemContent);
                    obj.GetComponent<InventoryItemController>().SetItem(item.Value.item);
                    obj.GetComponent<InventoryItemController>().SetRemoveButton();
                    obj.transform.GetChild(0).GetComponent<Image>().sprite = item.Value.item.icon;
                    obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.Value.item.maxStackeables.ToString();
                }
                if (item.Value.amount - (item.Value.item.maxStackeables * stacks) > 0) {

                    obj = Instantiate(prefab, itemContent);
                    obj.GetComponent<InventoryItemController>().SetItem(item.Value.item);
                    obj.GetComponent<InventoryItemController>().SetRemoveButton();
                    obj.transform.GetChild(0).GetComponent<Image>().sprite = item.Value.item.icon;
                    obj.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = (item.Value.amount - (item.Value.item.maxStackeables * stacks)).ToString();
                }
            }
        }
    }

    private void OrderDictionary(int i) {

        Dictionary<string, Items> sortedDict=new Dictionary<string, Items>();
        switch (i) {
            case 0: //id
                sortedDict = _items.OrderBy(x => x.Value.item.id).ToDictionary(x => x.Key, x => x.Value);
              
                break;
            case 1: //Name
                sortedDict = _items.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        
                break;
            case 2: //Quantity
                sortedDict = _items.OrderByDescending(x => x.Value.amount).ToDictionary(x => x.Key, x => x.Value);
       
                break;
        }

        _items = sortedDict;
        ListItems();
    }

    public void ShowItemName(bool b, string stringKey) {

        itemNameText.GetComponent<ItemNameController>().SetTextOnMousePosition();
        itemNameText.GetComponentInChildren<TextIdiom>().SetStringKey(stringKey);
        itemNameText.SetActive(b);
    }

    public void SwitchOrder(int i) {

        order = i;
        OrderDictionary(order);
    }

    public int ReturnAmountOfItem(string name) {

        if (!_items.ContainsKey(name)) 
            return 0;
        else
            return _items[name].amount;
    }
}
