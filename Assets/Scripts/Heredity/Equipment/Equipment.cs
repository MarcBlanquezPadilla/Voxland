using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class Equipment : MonoBehaviour {

    public bool owned;
    public Item item;

    public GameObject hand;

	private void Awake() {

        hand = gameObject.transform.parent.gameObject;
	}

	public void Equip(GameObject player) {

        ActiveObject(player);
    }

    public abstract void Use();

    public void ActiveObject(GameObject player) {

        for (int i = 0; i < hand.transform.childCount; i++)
        {

            hand.transform.GetChild(i).gameObject.SetActive(false);
        }

        gameObject.SetActive(true);
    }

    public void OwnEquip(bool value) {

        owned = value;
    }
}
