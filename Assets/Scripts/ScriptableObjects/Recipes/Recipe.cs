using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Needed
{
    public Item itemNeeded;
    public int amount;
}

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe/Create New Recipe")]
public class Recipe : ScriptableObject {

    public List<Needed> needed;
    public Item craftingObj;
    public int value;
}
