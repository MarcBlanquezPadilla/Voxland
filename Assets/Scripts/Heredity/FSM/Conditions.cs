using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM {

    public abstract class Conditions : ScriptableObject {

        public abstract bool Condition(Controller controller);
    }
}
