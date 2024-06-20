using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM {

    [System.Serializable]
    public class Transition {

        public Conditions condition;
        public State trueState;
    }
}
