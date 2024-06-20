using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextIdiom : MonoBehaviour
{
	private TextMeshProUGUI _tmp;
	public string stringKey;

    private void Awake()
    {
		TextManager.Instance.SetTextToList(this);
    }

    private void OnEnable()
    {
		UpdateText();
	}

    public void RemoveFromList()
    {
		TextManager.Instance.RemoveTextToList(this);
	}

    public void UpdateText()
	{
		_tmp = GetComponent<TextMeshProUGUI>();
		_tmp.text = TextManager.Instance.GetText(stringKey);
	}

	public void SetStringKey(string s)
    {
		stringKey = s;
		UpdateText();
	}
}
