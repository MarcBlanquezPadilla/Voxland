using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour {

	[SerializeField] private float speed;
	private RectTransform rectTransform;
	[SerializeField] private RectTransform target;

	private void Awake() {

		rectTransform = GetComponent<RectTransform>();
	}

	private void Update() {

		rectTransform.transform.position = Vector3.MoveTowards(rectTransform.transform.position, target.transform.position, Time.deltaTime * speed * Screen.height * 0.1f);
	}
}
