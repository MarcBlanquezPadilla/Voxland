using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFade : MonoBehaviour {

	[Header("Properties")]
	[SerializeField] private float fadeSpeed = 5;

	private CanvasGroup canvasGroup;

	private void Awake() {

		canvasGroup = GetComponent<CanvasGroup>();
	}

	private void OnEnable() {

		canvasGroup.alpha = 0;
		StopAllCoroutines();
		StartCoroutine(FadeCanvas(1));
	}

	public void CloseFading() {

		StopAllCoroutines();
		if (this.gameObject.activeInHierarchy)
			StartCoroutine(FadeCanvas(0));
	}

	public void StopFading() {

		StopAllCoroutines();
	}

	IEnumerator FadeCanvas(int target) {

		canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime * fadeSpeed);

		yield return new WaitForSeconds(0);

		if (target == 1) {

			canvasGroup.blocksRaycasts = true;
			canvasGroup.interactable = true;

			if (canvasGroup.alpha < target)
				StartCoroutine(FadeCanvas(target));

			if (canvasGroup.alpha >= target)
				canvasGroup.alpha = 1;
		}
		else if (target == 0) {

			canvasGroup.blocksRaycasts = false;
			canvasGroup.interactable = false;

			if (canvasGroup.alpha > target)
				StartCoroutine(FadeCanvas(target));

			if (canvasGroup.alpha <= target)
				gameObject.SetActive(false);
		}
	}
}
