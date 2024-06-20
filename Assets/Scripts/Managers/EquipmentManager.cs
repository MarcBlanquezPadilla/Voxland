using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EquipmentManager : MonoBehaviour {

    #region Instance

    private static EquipmentManager _instance;
    public static EquipmentManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EquipmentManager>();
            }
            return _instance;
        }
    }

    #endregion

    [Header("Properties")]
    [SerializeField] private List<Equipment> equipment = new List<Equipment>();
    [SerializeField] private int currentEquipment = 0;

    [Header("References")]
    [SerializeField] private EquipmentIconeController _ei;

    private void Start() {

        for (int i = 0; i < equipment.Count; i++) {

            equipment[i].gameObject.SetActive(false);
        }

        equipment[currentEquipment].gameObject.SetActive(true);
    }

    public void NextEquip() {

        currentEquipment = (currentEquipment + 1) % equipment.Count;

        for (int i = 0; i < equipment.Count; i++) {

            if (equipment[currentEquipment].owned) {

                equipment[currentEquipment].ActiveObject(GameManager.Instance.player);
                i = equipment.Count;
            }
            else {

                currentEquipment = (currentEquipment + 1) % equipment.Count;
            }
        }

        ChangeIcone();
    }

    public void PreviousEquip() {

        currentEquipment = (currentEquipment - 1);
        if (currentEquipment < 0) currentEquipment = equipment.Count - 1;


        for (int i = 0; i < equipment.Count; i++) {

            if (equipment[currentEquipment].owned) {

                equipment[currentEquipment].ActiveObject(GameManager.Instance.player);
                i = equipment.Count;
            }
            else {

                currentEquipment = (currentEquipment - 1);
                if (currentEquipment < 0) currentEquipment = equipment.Count - 1;
            }
        }

        ChangeIcone();
    }

    public void EnableEquip(Item item)
    {
        for (int i = 0; i < equipment.Count; i++)
        {
            if (equipment[i].item == item)
            {
                equipment[i].owned = true;
            }
        }
    }

    public void DisableEquip(Item item)
    {
        for (int i = 0; i < equipment.Count; i++)
        {
            if (equipment[i].item == item)
            {
                equipment[i].owned = false;
            }
        }
        NextEquip();
    }

    public void UseEquip(){

        equipment[currentEquipment].Use();
    }

    private void ChangeIcone() {

        //_ei.ChangeIconeImage(equipment[currentEquipment].iconeImage);
    }
}
