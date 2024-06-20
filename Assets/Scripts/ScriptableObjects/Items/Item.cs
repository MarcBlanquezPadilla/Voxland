using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject {

    public int id;
    public string itemName;
    public string stringKey;
    public int value;
    public Sprite icon;
    public ItemType itemType;
    public int maxStackeables;
    public GameObject itemObj;

    public enum ItemType { 
    
        Default,
        Resource,
        Weapon,
        Potion,
        NotDropeable,
        Map
    }
}
