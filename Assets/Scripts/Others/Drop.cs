using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dropeables {

    public Item item;
    public int quantity;
    public bool fixedDrop;
}

public class Drop : MonoBehaviour {

    [SerializeField] private List<Dropeables> dropeablesList;
    [SerializeField] private Transform droppingPoint;

    private Transform parent;
    private int amount;

    private void Awake() {

        parent = GameObject.Find("Dropeables").transform;
        amount = 0;
    }

    public void DropObjects() {

        for (int i = 0; i < dropeablesList.Count; i++) {

            if (dropeablesList[i].fixedDrop)
                amount = Random.Range(1, dropeablesList[i].quantity);
            else
                amount = Random.Range(0, dropeablesList[i].quantity);

            for (int j = 0; j < amount; j++) {

                GameObject dropeable = GameObject.Instantiate(dropeablesList[i].item.itemObj, parent);
                dropeable.transform.Rotate(dropeable.transform.rotation.x, Random.Range(0, 360), dropeable.transform.rotation.z, 0);
                dropeable.transform.position = new Vector3(droppingPoint.position.x + Random.Range(-1, 2), droppingPoint.position.y, droppingPoint.position.z + Random.Range(-1, 2));
            }
        }
    }
}
