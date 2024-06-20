using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

    public Vector3 offset;
    public CanvasGroup canvasGroup;

	private bool child = false;

	private void Awake() {

		if (canvasGroup == null) {

			if (GetComponent<CanvasGroup>() == null) {

				gameObject.AddComponent<CanvasGroup>();
				canvasGroup = GetComponent<CanvasGroup>();
			}
			else {

				canvasGroup = GetComponent<CanvasGroup>();
			}
		}
		else {

			child = true;
		}
	}

	public void OnDrag(PointerEventData eventData) {

		if(eventData.button == PointerEventData.InputButton.Left)
			if (child)
				transform.parent.position = Input.mousePosition + offset;
			else
				transform.position = Input.mousePosition + offset; ;
	}

	public void OnPointerDown(PointerEventData eventData) {

		if (eventData.button == PointerEventData.InputButton.Left) {
			if (child)
				offset = transform.parent.position - Input.mousePosition;
			else
				offset = transform.position - Input.mousePosition;

			canvasGroup.alpha = 0.5f;
		}
	}

	public void OnPointerUp(PointerEventData eventData) {

		canvasGroup.alpha = 1;
		var height = Screen.height;
		var width = Screen.width;

		if (child)
			if (canvasGroup.transform.parent.position.x < 0 || canvasGroup.transform.parent.position.x > width || canvasGroup.transform.parent.position.y < 0 || canvasGroup.transform.parent.position.y > height / 1.5f)
				canvasGroup.transform.parent.localPosition = new Vector2(0, 0);
		else
			if (canvasGroup.transform.position.x < 0 || canvasGroup.transform.position.x > width || canvasGroup.transform.position.y < 0 || canvasGroup.transform.position.y > height / 1.5f)
				canvasGroup.transform.localPosition = new Vector2(0, 0);

		//canvasGroup.transform.position = new Vector2(width / 2, height / 2);
	}
}
