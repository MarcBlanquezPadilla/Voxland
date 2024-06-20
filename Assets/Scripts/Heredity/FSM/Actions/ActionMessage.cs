using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Carnation/ActionMessage")]

public class ActionMessage : Action {

    public string message;

    public override void Act(Controller controller) {

        Debug.Log($"La saluda es: {controller.health}{message}");
    }
}
