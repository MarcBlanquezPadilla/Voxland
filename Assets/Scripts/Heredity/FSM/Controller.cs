using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM {
   
    public class Controller : MonoBehaviour {

        public State currentState;
        public State remainState;

        public float health;

        public bool ActiveObject { get; set; }

        virtual public void Start() {

            ActiveObject = true;
        }

        virtual public void Update() {

            if (!ActiveObject) return;
            if (currentState != null) { currentState.UpdateState(this);  }
        }

        public void Transition(State nextState) {

            if (nextState != remainState) {

                currentState = nextState;
            }
        }
    }
}
