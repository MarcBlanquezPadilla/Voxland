using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Carnation/State")]
    public class State : ScriptableObject {

        public Action[] actions;
        public Transition[] transitions;

        public void UpdateState(Controller controller) {

            DoActions(controller);
            CheckTransition(controller);
        }

        private void DoActions(Controller controller) {

            for (int i = 0; i < actions.Length; i++) {

                actions[i].Act(controller);
            }
        }

        private void CheckTransition(Controller controller) {

            for (int i = 0; i < transitions.Length; i++) {

                bool condition = transitions[i].condition.Condition(controller);

                if (condition) {

                    controller.Transition(transitions[i].trueState);
                    return;
                }
            }
        }
    }
}
