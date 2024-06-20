using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBarController : MonoBehaviour {

    [Header("Properties")]
    [SerializeField] private float lerpSpeed = 1;
    private Image fillBarImage;

    void Awake() {

        fillBarImage = GetComponent<Image>();
    }

    public void UpdateBar(float percent) {

        if (percent != fillBarImage.fillAmount) {

            StopAllCoroutines();
            StartCoroutine(UpdateBarCoroutine(percent));
        }
    }

    IEnumerator UpdateBarCoroutine(float percent) {

        fillBarImage.fillAmount = Mathf.MoveTowards(fillBarImage.fillAmount, percent, Time.deltaTime * lerpSpeed);
        
        if (Mathf.Abs(fillBarImage.fillAmount - percent) < 0.001)
            fillBarImage.fillAmount = percent;

        yield return new WaitForSeconds(0);

        if (fillBarImage.fillAmount != percent!)
            StartCoroutine(UpdateBarCoroutine(percent));
    }
}
