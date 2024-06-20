using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour {

    [SerializeField] private List<GameObject> pressurePlates;
    [SerializeField] private List<GameObject> pressureOrder;
    [SerializeField] private List<int> pressedPlates;
    [SerializeField] private UnityEvent onComplet;
    public bool enabledPuzzle = false;
    public void CheckPlates(int id) {

        if (enabledPuzzle) {

            pressedPlates.Add(id);
            AudioManager.Instance.playAudio("PressPlate");

            bool executed = false;

            for (int i = 0; i < pressedPlates.Count && !executed; i++)
            {

                if (pressedPlates[i] != pressureOrder[i].GetComponent<PressuperPlateController>().ReturnId()) {

                    pressedPlates.Clear();
                    AudioManager.Instance.playAudio("ResetPlates");
                }
                else if (pressureOrder.Count == pressedPlates.Count) {

                    float corectPlates = 0;

                    for (int j = 0; j < pressureOrder.Count; j++) {

                        if (pressedPlates[j] == pressureOrder[j].GetComponent<PressuperPlateController>().ReturnId()) {

                            corectPlates = j + 1;
                        }
                    }

                    if (corectPlates == pressureOrder.Count) {

                        onComplet.Invoke();
                        pressedPlates.Clear();
                        executed = true;
                    }                  
                }
            }
        }   
	}

    public void EnablePuzzle() {

        enabledPuzzle = true;
        AudioManager.Instance.playAudio("ResetPlates");
    }

    public void DisablePuzzle() {

        enabledPuzzle = false;
	}
}
