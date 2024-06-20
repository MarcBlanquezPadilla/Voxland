using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddExperienceLevels : MonoBehaviour {

    [SerializeField] private float experienceToAdd = 5;

    public void Add() {

        GameManager.Instance.player.GetComponent<ExperienceBehaviour>().AddXp(experienceToAdd);
    }

    public void ChangeExperienceToAdd(float xp) {

        experienceToAdd = xp;
    }
}
