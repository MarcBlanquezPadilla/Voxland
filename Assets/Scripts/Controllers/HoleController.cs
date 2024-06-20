using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour {

    public Color color;
    public bool state = false;

    public bool ReturnState() {

        return state;
    }

    public void ChangeState(bool value) {

        state = value;
    }

    public Color ReturnColor() {

        return color;
	}
}
