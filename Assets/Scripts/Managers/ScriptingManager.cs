using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptingManager : MonoBehaviour
{
    #region Instance

    private static ScriptingManager _instance;
    public static ScriptingManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScriptingManager>();
            }
            return _instance;
        }
    }

    #endregion

    public static bool scriptingMode;

    private Script[] scripts;
    Script script;

    void Start()
    {
        scripts = new Script[transform.childCount];
        scripts = GetComponentsInChildren<Script>();

        scriptingMode = false;
        script = null;
    }

    public void StartScript(Script s)
    {
        if (s != null)
        {
            script = s;
            scriptingMode = true;
            script.Run();
        }
    }

    public void StopScript()
    {
        scriptingMode = false;
        script = null;
    }
}
