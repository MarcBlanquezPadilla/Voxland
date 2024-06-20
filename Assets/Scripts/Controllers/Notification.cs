using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    private CanvasFade _canvasFade;

    public Image notificationIcon;
    public float timeToStartFade;

    private void OnEnable()
    {
        _canvasFade = GetComponent<CanvasFade>();
        StartCoroutine(WaitAndFade());
    }

    private IEnumerator WaitAndFade()
    {
        yield return new WaitForSeconds(timeToStartFade);
        _canvasFade.CloseFading();

    }
}
