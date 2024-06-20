using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text : MonoBehaviour {

    private TextMeshProUGUI _tmp;

	private void Awake() {

		_tmp = GetComponent<TextMeshProUGUI>();
	}

	public void IntToText(int text) {

		_tmp.text = text.ToString();
    }
}
