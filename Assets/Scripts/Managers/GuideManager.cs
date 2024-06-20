using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Guide
{
    public string name;
    [System.NonSerialized] public bool did = false;
    public string stringKey;
}

public class GuideManager : MonoBehaviour {

    #region Instance

    private static GuideManager _instance;
    public static GuideManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GuideManager>();
            }
            return _instance;
        }
    }

    #endregion

    [SerializeField] private GameObject guideObj;
    [SerializeField] private GameObject guideTextObj;
    [SerializeField] private GameObject hideCrossObj;

    [SerializeField] private List<Guide> guide;

    [SerializeField] private float timeToHide;
    [SerializeField] private float timeToShow;

    bool changing = false;
    bool hide = false;

    private void Awake()
    {
        guideObj.SetActive(true);
        guideTextObj.SetActive(false);
        hideCrossObj.SetActive(false);

        ShowNewGuide(0);
    }

    public void ShowNewGuide(int guideNum) {

        if ((guideNum == 0 || guide[guideNum-1].did) && !guide[guideNum].did)
        {
            guide[guideNum].did = true;
            changing = true;
            StopAllCoroutines();
            StartCoroutine(ChangeGuide(guideNum));
        }
    }

    private IEnumerator ChangeGuide(int guideNum)
    {
        if (guideTextObj.activeInHierarchy)
        {
            guideTextObj.GetComponent<CanvasFade>().CloseFading();
            yield return new WaitForSeconds(timeToShow);
            guideTextObj.GetComponentInChildren<TextIdiom>().SetStringKey(guide[guideNum].stringKey);
            guideTextObj.GetComponentInChildren<TextIdiom>().UpdateText();
            guideTextObj.SetActive(true);
        }
        else
        {
            guideTextObj.GetComponentInChildren<TextIdiom>().SetStringKey(guide[guideNum].stringKey);
            guideTextObj.GetComponentInChildren<TextIdiom>().UpdateText();
            guideTextObj.SetActive(true);
        }

        if (hide)
        {
            StartCoroutine(HideGuide());
        }
    }

    public void SwitchHideMode()
    {
        if (changing)
        {
            hide = !hide;
            if (hide)
            {
                Hide();
            }
            else
            {
                UnHide();
            }
            hideCrossObj.SetActive(hide);
        }   
    }

    public IEnumerator HideGuide()
    {
        yield return new WaitForSeconds(timeToHide);
        Hide();
    }

    public void UnHide()
    {
        guideTextObj.SetActive(true);
    }

    public void Hide()
    {
        if (hide && guideTextObj.activeInHierarchy)
        {
            guideTextObj.GetComponent<CanvasFade>().CloseFading();
        }
    }

    public void DisableGuide()
    {
        guideObj.GetComponent<CanvasFade>().CloseFading();
    }

    public void EnableGuide()
    {
        guideObj.SetActive(true);
    }
}
