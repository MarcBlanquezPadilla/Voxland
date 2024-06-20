using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentIconeController : MonoBehaviour {

	private Image image;

	private void Awake() {

		image = GetComponent<Image>();
	}

	public void ChangeIconeImage(Sprite newImage) {

		image.sprite = newImage;
	}
}

