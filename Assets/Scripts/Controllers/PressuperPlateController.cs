using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressuperPlateController : MonoBehaviour {

	[SerializeField] private int id;
	[SerializeField] private UnityEvent<int> onPress;

	private void OnTriggerEnter(Collider other) {

		onPress.Invoke(id);
	}

	public int ReturnId() {

		return id;
	}
}
