using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Carnation/ConditionHealth")]

public class ConditionHealth : Conditions {

    public int h;

    public override bool Condition(Controller controller) {

        if (controller.health <= h) return true;
        else return false;
    }
}
