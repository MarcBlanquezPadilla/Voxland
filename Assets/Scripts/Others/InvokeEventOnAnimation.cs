using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeEventOnAnimation : MonoBehaviour {

    public UnityEvent animEvent;

    public void InvokeEvent() {

        animEvent.Invoke();
    }
}
