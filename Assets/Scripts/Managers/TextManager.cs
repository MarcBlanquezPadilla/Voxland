using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    #region Instance

    private static TextManager _instance;
    public static TextManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TextManager>();
            }
            return _instance;
        }
    }

    #endregion

    [SerializeField] private List<TextIdiom> listTexts = new List<TextIdiom>();

    [Header("PROPERTIES")]
    [SerializeField] int languageIndex = 0;

    [Header("REFERENCED")]
    [SerializeField] TextAsset texts;

    Dictionary<string, string[]> idToText;

    void Awake()
    {
        idToText = new Dictionary<string, string[]>();

        string[] lines = texts.text.Split('\n');
        
        for (int i = 0; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('#');

            if (parts.Length >= 2)
            {
                string[] versions = new string[parts.Length - 1];

                for (int v = 0; v < versions.Length; v++)
                {
                    versions[v] = parts[1 + v];
                }

                idToText.Add(parts[0], versions);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeIdiom(0);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeIdiom(1);
        }
    }

    public void ChangeIdiom (int idiom)
    {
        languageIndex = idiom;
        foreach(TextIdiom text in listTexts)
        {
            text.UpdateText();
        }
    }

    public string GetText(string id)
    {
        return idToText[id][languageIndex];
    }

    public void SetTextToList(TextIdiom text)
    {
        listTexts.Add(text);
    }

    public void RemoveTextToList(TextIdiom text)
    {
        listTexts.Remove(text);
    }
}
