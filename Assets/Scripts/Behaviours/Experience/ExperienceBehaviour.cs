using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExperienceBehaviour : MonoBehaviour {

    [Header("Properties")]
    [SerializeField] private float experience = 0;
    [SerializeField] private int level = 0;
    [SerializeField] private float neededExperience = 100;

    [Header("Events")]
    [SerializeField] private UnityEvent<float> onChangeExperience;
    [SerializeField] private UnityEvent<int> onChangeLevel;
    [SerializeField] private UnityEvent<float> updateExperienceIndicator;

    void Awake() {

        onChangeLevel.Invoke(level);
        updateExperienceIndicator.Invoke(ReturnXpPercent());
    }

    public void WasteXp(float xpToWaste) {

        experience -= xpToWaste;

        if (experience < 0)
            experience = 0;

        onChangeExperience.Invoke(experience);
        updateExperienceIndicator.Invoke(ReturnXpPercent());
    }

    public void AddXp(float xpToAdd) {

        float remainingExperience = xpToAdd + experience;
        experience = 0;

        do {

            if (remainingExperience > neededExperience) {

                remainingExperience -= neededExperience;
                UpLevel();
            }
            else {

                if (remainingExperience != 0) 
                    experience = remainingExperience;

                remainingExperience = 0;
            }
        }
        while (remainingExperience != 0);

        onChangeExperience.Invoke(experience);
        updateExperienceIndicator.Invoke(ReturnXpPercent());
        onChangeLevel.Invoke(level);
    }

    public void LoseLevels(int lostLevels) {

        level -= lostLevels;
        neededExperience -= 100 * lostLevels;
        experience = 0;

        if (level < 1) {

            level = 0;
            neededExperience = 100;
        }

        updateExperienceIndicator.Invoke(ReturnXpPercent());
        onChangeLevel.Invoke(level);
    }

    public void UpLevel() {

        level++;
        neededExperience += 100;
        onChangeLevel.Invoke(level);
    }

    public void SetLevel(int lvl) {

        level = lvl;
    }

    public float ReturnXpPercent() {

        return experience / neededExperience;
    }

    public int ReturnLevel() {

        return level;
    }
}
