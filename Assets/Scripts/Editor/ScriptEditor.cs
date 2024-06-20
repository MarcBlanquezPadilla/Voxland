using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScriptEditor : EditorWindow
{
    static Script script;
    static Script.Command copiedCommand;

    [MenuItem("Window/Script Editor")]
    public static void Open()
    {
        ScriptEditor s = GetWindow<ScriptEditor>();

        s.Show();
    }

    ScriptEditor()
    {
        Selection.selectionChanged += OnSelectionChanged;
    }

    private void OnDestroy()
    {
        Selection.selectionChanged += OnSelectionChanged;
    }

    private void OnGUI()
    {
        if (Selection.activeTransform == null)
        {
            EditorGUILayout.HelpBox("Selecciona un objeto", MessageType.Warning);
            return;
        }

        script = Selection.activeTransform.GetComponent<Script>();

        if (script == null)
        {
            EditorGUILayout.HelpBox("El Objeto no tienen un componente script", MessageType.Warning);
            return;
        }


        bool up = false;
        bool down = false;
        bool insert = false;
        bool delete = false;
        bool copy = false;
        bool paste = false;
        int targetIndex = 0;

        Script.Command[] commands = script.EditorGetCommands();

        for (int i = 0; i < commands.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();

            Script.Command c = commands[i];

            bool deletePressed;
            deletePressed = GUILayout.Button("Delete");

            EditorGUILayout.LabelField("" + i, GUILayout.Width(10));
            c.id = (Script.CommandId)EditorGUILayout.EnumPopup(commands[i].id, GUILayout.Width(50));
            c.param1 = EditorGUILayout.FloatField(commands[i].param1, GUILayout.Width(30));
            c.param2 = EditorGUILayout.FloatField(commands[i].param2, GUILayout.Width(30));
            c.param3 = EditorGUILayout.FloatField(commands[i].param3, GUILayout.Width(30));
            c.stringParam1 = EditorGUILayout.TextField(commands[i].stringParam1, GUILayout.Width(120));
            c.stringParam2 = EditorGUILayout.TextField(commands[i].stringParam2, GUILayout.Width(120));
            c.stringParam3 = EditorGUILayout.TextField(commands[i].stringParam3, GUILayout.Width(120));
            c.objectParam1 = (GameObject)EditorGUILayout.ObjectField(commands[i].objectParam1, typeof(GameObject), GUILayout.Width(70));
            c.objectParam2 = (GameObject)EditorGUILayout.ObjectField(commands[i].objectParam2, typeof(GameObject), GUILayout.Width(70));
            c.objectParam3 = (GameObject)EditorGUILayout.ObjectField(commands[i].objectParam3, typeof(GameObject), GUILayout.Width(70));
            c.executeWithPrevious = EditorGUILayout.Toggle(commands[i].executeWithPrevious, GUILayout.Width(20));
            

            if (EditorGUI.EndChangeCheck())
            {
                commands[i] = c;
            }

            bool upPressed;
            bool downPressed;
            bool insertPressed;
            bool copyPressed;
            bool pastePressed;

            upPressed = GUILayout.Button("Up");
            downPressed = GUILayout.Button("Down");
            insertPressed = GUILayout.Button("Insert");
            copyPressed = GUILayout.Button("Copy");
            pastePressed = GUILayout.Button("Paste");

            if (upPressed) { up = true; targetIndex = i; }
            else if (downPressed) { down = true; targetIndex = i; }
            else if (insertPressed) { insert = true; targetIndex = i; }
            else if (deletePressed) { delete = true; targetIndex = i; }
            else if (copyPressed) { copy = true; targetIndex = i; }
            else if (pastePressed) { paste = true; targetIndex = i; }

            EditorGUILayout.EndHorizontal();
        }

        bool newPressed;
        newPressed = GUILayout.Button("New");



        if (up && targetIndex > 0)
        {
            Script.Command c1 = commands[targetIndex];
            Script.Command c2 = commands[targetIndex-1];

            commands[targetIndex] = c2;
            commands[targetIndex-1] = c1;
        }

        if (down && targetIndex < commands.Length)
        {
            Script.Command c1 = commands[targetIndex];
            Script.Command c2 = commands[targetIndex + 1];

            commands[targetIndex] = c2;
            commands[targetIndex + 1] = c1;
        }

        if (insert)
        {
            Script.Command[] nextCommands = new Script.Command[commands.Length + 1];

            for (int i = 0; i< nextCommands.Length; i++)
            {
                if (i < targetIndex + 1) { nextCommands[i] = commands[i]; }
                else if (i > targetIndex + 1) { nextCommands[i] = commands[i - 1]; }
                else
                    nextCommands[i] = new Script.Command();
            }

            script.EditorSetCommands(nextCommands);
        }

        if (delete)
        {
            Script.Command[] nextCommands = new Script.Command[commands.Length - 1];

            for (int i = 0; i < commands.Length; i++)
            {
                if (i < targetIndex) { nextCommands[i] = commands[i]; }
                else if (i > targetIndex) { nextCommands[i - 1] = commands[i]; }
            }

            script.EditorSetCommands(nextCommands);
        }

        if (copy)
        {
            copiedCommand = commands[targetIndex];
        }

        if (paste)
        {
            commands[targetIndex] = copiedCommand;
        }

        if (newPressed)
        {
            Script.Command[] nextCommands = new Script.Command[commands.Length + 1];
            for (int i = 0; i < commands.Length; i++)
            {
                nextCommands[i] = commands[i];
            }
            nextCommands[commands.Length] = new Script.Command();
            script.EditorSetCommands(nextCommands);
        }
    }

    private void OnSelectionChanged()
    {
        Repaint();
    }
}
