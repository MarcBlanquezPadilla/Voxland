using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNameController : MonoBehaviour {

    public RectTransform rectTransform;

    private void FixedUpdate() {
        if (!ScriptingManager.scriptingMode)
            rectTransform.position = Input.mousePosition;
    }

    public void SetTextOnMousePosition() {
        rectTransform.position = Input.mousePosition;
    }
}
